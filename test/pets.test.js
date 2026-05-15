import assert from 'node:assert/strict';
import { after, before, describe, it } from 'node:test';
import { signAdminToken } from '../src/auth/jwt.js';
import { createApp } from '../src/app.js';

const listen = (server) =>
  new Promise((resolve) => {
    server.listen(0, () => resolve(server.address().port));
  });

const close = (server) => new Promise((resolve, reject) => server.close((error) => (error ? reject(error) : resolve())));

const requestJson = async (baseUrl, path, { method = 'GET', token, body, headers = {} } = {}) => {
  const response = await fetch(`${baseUrl}${path}`, {
    method,
    headers: {
      ...(body ? { 'content-type': 'application/json' } : {}),
      ...(token ? { authorization: `Bearer ${token}` } : {}),
      ...headers
    },
    body: body ? JSON.stringify(body) : undefined
  });

  if (response.status === 204) {
    return { status: response.status, body: undefined };
  }

  return { status: response.status, body: await response.json() };
};

describe('pet HTTP API', () => {
  let server;
  let baseUrl;
  const token = signAdminToken('test-admin');

  before(async () => {
    server = createApp();
    const port = await listen(server);
    baseUrl = `http://127.0.0.1:${port}`;
  });

  after(async () => {
    await close(server);
  });

  it('protects administrative endpoints with JWT', async () => {
    const response = await requestJson(baseUrl, '/api/pets');
    assert.equal(response.status, 401);
    assert.equal(response.body.error, 'Missing bearer token');
  });

  it('rejects bearer tokens with extra JWT segments', async () => {
    const response = await requestJson(baseUrl, '/api/pets', { token: `${token}.extra` });
    assert.equal(response.status, 401);
    assert.equal(response.body.error, 'Invalid bearer token');
  });

  it('supports the main admin CRUD flow', async () => {
    const createResponse = await requestJson(baseUrl, '/api/pets', {
      method: 'POST',
      token,
      body: {
        publicCode: 'BELINHA-QR',
        name: 'Belinha',
        species: 'dog',
        breed: 'SRD',
        color: 'caramel',
        description: 'Dócil e usa coleira vermelha',
        status: 'missing',
        medicalNotes: 'Alergia medicamentosa',
        privateNotes: 'Não divulgar endereço completo',
        tutor: {
          name: 'Maria Silva',
          document: '12345678900',
          email: 'maria@example.com',
          phone: '+55 11 99999-0000',
          contacts: [
            { kind: 'whatsapp', value: '+55 11 99999-0000', label: 'WhatsApp', public: true },
            { kind: 'email', value: 'maria@example.com', label: 'E-mail privado', public: false }
          ]
        }
      }
    });

    assert.equal(createResponse.status, 201);
    assert.equal(createResponse.body.name, 'Belinha');
    assert.equal(createResponse.body.medicalNotes, 'Alergia medicamentosa');
    assert.equal(createResponse.body.tutor.document, '12345678900');

    const id = createResponse.body.id;

    const getResponse = await requestJson(baseUrl, `/api/pets/${id}`, { token });
    assert.equal(getResponse.status, 200);
    assert.equal(getResponse.body.publicCode, 'BELINHA-QR');

    const updateResponse = await requestJson(baseUrl, `/api/pets/${id}`, {
      method: 'PUT',
      token,
      body: { status: 'reunited', tutor: { contacts: [{ kind: 'phone', value: '+55 11 98888-0000', public: true }] } }
    });
    assert.equal(updateResponse.status, 200);
    assert.equal(updateResponse.body.status, 'reunited');
    assert.deepEqual(updateResponse.body.tutor.contacts, [{ kind: 'phone', value: '+55 11 98888-0000', public: true }]);

    const listResponse = await requestJson(baseUrl, '/api/pets', { token });
    assert.equal(listResponse.status, 200);
    assert.equal(listResponse.body.length, 1);

    const deleteResponse = await requestJson(baseUrl, `/api/pets/${id}`, { method: 'DELETE', token });
    assert.equal(deleteResponse.status, 204);

    const notFoundResponse = await requestJson(baseUrl, `/api/pets/${id}`, { token });
    assert.equal(notFoundResponse.status, 404);
  });

  it('returns only public data through QR Code endpoint and records ScanHistory', async () => {
    const createResponse = await requestJson(baseUrl, '/api/pets', {
      method: 'POST',
      token,
      body: {
        publicCode: 'TICO-QR',
        name: 'Tico',
        species: 'cat',
        privateNotes: 'Tutor prefere contato após 18h',
        tutor: {
          name: 'João Tutor',
          document: '98765432100',
          email: 'joao@example.com',
          contacts: [
            { kind: 'phone', value: '+55 21 97777-0000', label: 'Celular', public: true },
            { kind: 'email', value: 'joao@example.com', label: 'E-mail interno', public: false }
          ]
        }
      }
    });
    const petId = createResponse.body.id;

    const publicResponse = await requestJson(baseUrl, '/api/public/pets/TICO-QR', {
      headers: { 'user-agent': 'qr-reader-test' }
    });

    assert.equal(publicResponse.status, 200);
    assert.equal(publicResponse.body.publicCode, 'TICO-QR');
    assert.equal(publicResponse.body.tutor.name, 'João Tutor');
    assert.deepEqual(publicResponse.body.tutor.contacts, [
      { kind: 'phone', value: '+55 21 97777-0000', label: 'Celular' }
    ]);
    assert.equal(publicResponse.body.privateNotes, undefined);
    assert.equal(publicResponse.body.medicalNotes, undefined);
    assert.equal(publicResponse.body.tutor.document, undefined);
    assert.equal(publicResponse.body.tutor.email, undefined);

    const scansResponse = await requestJson(baseUrl, `/api/pets/${petId}/scans`, { token });
    assert.equal(scansResponse.status, 200);
    assert.equal(scansResponse.body.length, 1);
    assert.equal(scansResponse.body[0].petId, petId);
    assert.equal(scansResponse.body[0].publicCode, 'TICO-QR');
    assert.equal(scansResponse.body[0].userAgent, 'qr-reader-test');
  });
});
