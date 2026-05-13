import { createHmac, timingSafeEqual } from 'node:crypto';

const DEFAULT_JWT_SECRET = 'cade-meu-pet-development-secret';
const textEncoder = new TextEncoder();

const base64UrlEncode = (input) =>
  Buffer.from(typeof input === 'string' ? input : JSON.stringify(input))
    .toString('base64url');

const base64UrlDecodeJson = (input) => JSON.parse(Buffer.from(input, 'base64url').toString('utf8'));

const sign = (data, secret) => createHmac('sha256', secret).update(data).digest('base64url');

const safeEqual = (left, right) => {
  const leftBytes = textEncoder.encode(left);
  const rightBytes = textEncoder.encode(right);
  return leftBytes.length === rightBytes.length && timingSafeEqual(leftBytes, rightBytes);
};

export const jwtSecret = () => process.env.JWT_SECRET ?? DEFAULT_JWT_SECRET;

export const signAdminToken = (subject = 'admin') => {
  const now = Math.floor(Date.now() / 1000);
  const header = { alg: 'HS256', typ: 'JWT' };
  const payload = {
    sub: subject,
    role: 'admin',
    iat: now,
    exp: now + 60 * 60
  };
  const unsignedToken = `${base64UrlEncode(header)}.${base64UrlEncode(payload)}`;
  return `${unsignedToken}.${sign(unsignedToken, jwtSecret())}`;
};

export const verifyAdminToken = (token) => {
  const [headerSegment, payloadSegment, signature] = token.split('.');

  if (!headerSegment || !payloadSegment || !signature) {
    throw new Error('Malformed token');
  }

  const unsignedToken = `${headerSegment}.${payloadSegment}`;
  const expectedSignature = sign(unsignedToken, jwtSecret());

  if (!safeEqual(signature, expectedSignature)) {
    throw new Error('Invalid token signature');
  }

  const header = base64UrlDecodeJson(headerSegment);
  const payload = base64UrlDecodeJson(payloadSegment);

  if (header.alg !== 'HS256' || payload.role !== 'admin' || !payload.sub) {
    throw new Error('Invalid admin token');
  }

  if (typeof payload.exp === 'number' && payload.exp < Math.floor(Date.now() / 1000)) {
    throw new Error('Expired token');
  }

  return { sub: payload.sub, role: payload.role };
};
