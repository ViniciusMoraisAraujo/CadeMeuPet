import { verifyAdminToken } from '../auth/jwt.js';
import {
  toAdminPetDto,
  toPublicPetDto,
  toScanHistoryDto,
  validatePetCreate,
  validatePetUpdate
} from '../dto/petDto.js';

const json = (response, statusCode, body) => {
  response.writeHead(statusCode, { 'content-type': 'application/json; charset=utf-8' });
  response.end(JSON.stringify(body));
};

const noContent = (response) => {
  response.writeHead(204);
  response.end();
};

const readJsonBody = async (request) => {
  const chunks = [];
  for await (const chunk of request) chunks.push(chunk);
  if (!chunks.length) return {};
  return JSON.parse(Buffer.concat(chunks).toString('utf8'));
};

const requireAdmin = (request, response) => {
  const authorization = request.headers.authorization;
  const [scheme, token] = authorization?.split(' ') ?? [];

  if (scheme !== 'Bearer' || !token) {
    json(response, 401, { error: 'Missing bearer token' });
    return false;
  }

  try {
    verifyAdminToken(token);
    return true;
  } catch {
    json(response, 401, { error: 'Invalid bearer token' });
    return false;
  }
};

const validateBody = async (request, response, validator) => {
  let body;
  try {
    body = await readJsonBody(request);
  } catch {
    json(response, 400, { error: 'Invalid JSON body' });
    return undefined;
  }

  const result = validator(body);
  if (!result.ok) {
    json(response, 400, { error: 'Invalid request body', details: result.errors });
    return undefined;
  }

  return result.data;
};

export const handlePetRoutes = async ({ request, response, repository, pathname }) => {
  const publicMatch = pathname.match(/^\/api\/public\/pets\/([^/]+)$/);
  if (request.method === 'GET' && publicMatch) {
    const pet = repository.findByPublicCode(decodeURIComponent(publicMatch[1]));
    if (!pet) return json(response, 404, { error: 'Pet not found' });

    repository.registerScan(pet, {
      ipAddress: request.socket.remoteAddress,
      userAgent: request.headers['user-agent']
    });

    return json(response, 200, toPublicPetDto(pet));
  }

  if (!pathname.startsWith('/api/pets')) {
    return false;
  }

  if (!requireAdmin(request, response)) {
    return true;
  }

  try {
    if (request.method === 'GET' && pathname === '/api/pets') {
      return json(response, 200, repository.list().map(toAdminPetDto));
    }

    if (request.method === 'POST' && pathname === '/api/pets') {
      const body = await validateBody(request, response, validatePetCreate);
      if (!body) return true;
      return json(response, 201, toAdminPetDto(repository.create(body)));
    }

    const scansMatch = pathname.match(/^\/api\/pets\/([^/]+)\/scans$/);
    if (request.method === 'GET' && scansMatch) {
      const pet = repository.findById(decodeURIComponent(scansMatch[1]));
      if (!pet) return json(response, 404, { error: 'Pet not found' });
      return json(response, 200, repository.listScans(pet.id).map(toScanHistoryDto));
    }

    const petMatch = pathname.match(/^\/api\/pets\/([^/]+)$/);
    if (petMatch) {
      const id = decodeURIComponent(petMatch[1]);

      if (request.method === 'GET') {
        const pet = repository.findById(id);
        return pet ? json(response, 200, toAdminPetDto(pet)) : json(response, 404, { error: 'Pet not found' });
      }

      if (request.method === 'PUT') {
        const body = await validateBody(request, response, validatePetUpdate);
        if (!body) return true;
        const pet = repository.update(id, body);
        return pet ? json(response, 200, toAdminPetDto(pet)) : json(response, 404, { error: 'Pet not found' });
      }

      if (request.method === 'DELETE') {
        return repository.delete(id) ? noContent(response) : json(response, 404, { error: 'Pet not found' });
      }
    }
  } catch (error) {
    if (error.statusCode === 409) return json(response, 409, { error: error.message });
    throw error;
  }

  return json(response, 405, { error: 'Method not allowed' });
};
