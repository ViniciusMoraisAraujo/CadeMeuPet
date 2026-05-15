import { createServer } from 'node:http';
import { PetRepository } from './repositories/petRepository.js';
import { handlePetRoutes } from './routes/pets.js';

const json = (response, statusCode, body) => {
  response.writeHead(statusCode, { 'content-type': 'application/json; charset=utf-8' });
  response.end(JSON.stringify(body));
};

export const createApp = ({ repository = new PetRepository() } = {}) =>
  createServer(async (request, response) => {
    response.setHeader('access-control-allow-origin', '*');
    response.setHeader('access-control-allow-headers', 'authorization, content-type');
    response.setHeader('access-control-allow-methods', 'GET, POST, PUT, DELETE, OPTIONS');

    if (request.method === 'OPTIONS') {
      response.writeHead(204);
      response.end();
      return;
    }

    const url = new URL(request.url ?? '/', 'http://localhost');

    try {
      if (request.method === 'GET' && url.pathname === '/health') {
        return json(response, 200, { status: 'ok' });
      }

      const handled = await handlePetRoutes({ request, response, repository, pathname: url.pathname });
      if (handled !== false) return;

      return json(response, 404, { error: 'Not found' });
    } catch {
      return json(response, 500, { error: 'Internal server error' });
    }
  });
