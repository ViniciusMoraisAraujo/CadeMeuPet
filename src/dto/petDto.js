const allowedStatuses = new Set(['active', 'missing', 'reunited', 'inactive']);
const allowedContactKinds = new Set(['phone', 'whatsapp', 'email']);

const optionalString = (value, field, errors, { url = false, email = false } = {}) => {
  if (value === undefined || value === null || value === '') {
    return undefined;
  }
  if (typeof value !== 'string' || value.trim() === '') {
    errors.push(`${field} must be a non-empty string`);
    return undefined;
  }
  const normalized = value.trim();
  if (url) {
    try {
      new URL(normalized);
    } catch {
      errors.push(`${field} must be a valid URL`);
    }
  }
  if (email && !normalized.includes('@')) {
    errors.push(`${field} must be a valid email`);
  }
  return normalized;
};

const requiredString = (value, field, errors) => {
  const normalized = optionalString(value, field, errors);
  if (normalized === undefined) {
    errors.push(`${field} is required`);
  }
  return normalized;
};

const sanitizeContact = (contact, index, errors) => {
  if (!contact || typeof contact !== 'object' || Array.isArray(contact)) {
    errors.push(`tutor.contacts[${index}] must be an object`);
    return undefined;
  }

  if (!allowedContactKinds.has(contact.kind)) {
    errors.push(`tutor.contacts[${index}].kind is invalid`);
  }

  const value = requiredString(contact.value, `tutor.contacts[${index}].value`, errors);
  const label = optionalString(contact.label, `tutor.contacts[${index}].label`, errors);

  return {
    kind: contact.kind,
    value,
    label,
    public: Boolean(contact.public)
  };
};

const sanitizeTutor = (tutor, errors, partial = false) => {
  if (!tutor || typeof tutor !== 'object' || Array.isArray(tutor)) {
    if (!partial) {
      errors.push('tutor is required');
    }
    return undefined;
  }

  const sanitized = {};
  const name = partial
    ? optionalString(tutor.name, 'tutor.name', errors)
    : requiredString(tutor.name, 'tutor.name', errors);
  if (name !== undefined) sanitized.name = name;

  const id = optionalString(tutor.id, 'tutor.id', errors);
  const document = optionalString(tutor.document, 'tutor.document', errors);
  const email = optionalString(tutor.email, 'tutor.email', errors, { email: true });
  const phone = optionalString(tutor.phone, 'tutor.phone', errors);
  if (id !== undefined) sanitized.id = id;
  if (document !== undefined) sanitized.document = document;
  if (email !== undefined) sanitized.email = email;
  if (phone !== undefined) sanitized.phone = phone;

  if (tutor.contacts !== undefined) {
    if (!Array.isArray(tutor.contacts)) {
      errors.push('tutor.contacts must be an array');
    } else {
      sanitized.contacts = tutor.contacts
        .map((contact, index) => sanitizeContact(contact, index, errors))
        .filter(Boolean);
    }
  } else if (!partial) {
    sanitized.contacts = [];
  }

  return sanitized;
};

export const validatePetCreate = (body) => {
  const errors = [];
  if (!body || typeof body !== 'object' || Array.isArray(body)) {
    return { ok: false, errors: ['body must be an object'] };
  }

  const status = body.status ?? 'active';
  if (!allowedStatuses.has(status)) {
    errors.push('status is invalid');
  }

  const sanitized = {
    publicCode: optionalString(body.publicCode, 'publicCode', errors),
    name: requiredString(body.name, 'name', errors),
    species: requiredString(body.species, 'species', errors),
    breed: optionalString(body.breed, 'breed', errors),
    color: optionalString(body.color, 'color', errors),
    description: optionalString(body.description, 'description', errors),
    photoUrl: optionalString(body.photoUrl, 'photoUrl', errors, { url: true }),
    status,
    medicalNotes: optionalString(body.medicalNotes, 'medicalNotes', errors),
    privateNotes: optionalString(body.privateNotes, 'privateNotes', errors),
    tutor: sanitizeTutor(body.tutor, errors)
  };

  return errors.length ? { ok: false, errors } : { ok: true, data: sanitized };
};

export const validatePetUpdate = (body) => {
  const errors = [];
  if (!body || typeof body !== 'object' || Array.isArray(body)) {
    return { ok: false, errors: ['body must be an object'] };
  }

  const sanitized = {};
  for (const field of ['publicCode', 'name', 'species', 'breed', 'color', 'description', 'medicalNotes', 'privateNotes']) {
    if (body[field] !== undefined) sanitized[field] = optionalString(body[field], field, errors);
  }
  if (body.photoUrl !== undefined) sanitized.photoUrl = optionalString(body.photoUrl, 'photoUrl', errors, { url: true });
  if (body.status !== undefined) {
    if (!allowedStatuses.has(body.status)) errors.push('status is invalid');
    sanitized.status = body.status;
  }
  if (body.tutor !== undefined) sanitized.tutor = sanitizeTutor(body.tutor, errors, true);

  return errors.length ? { ok: false, errors } : { ok: true, data: sanitized };
};

export const toAdminPetDto = (pet) => ({ ...pet, tutor: { ...pet.tutor, contacts: [...pet.tutor.contacts] } });

export const toPublicPetDto = (pet) => ({
  publicCode: pet.publicCode,
  name: pet.name,
  species: pet.species,
  breed: pet.breed,
  color: pet.color,
  description: pet.description,
  photoUrl: pet.photoUrl,
  status: pet.status,
  tutor: {
    name: pet.tutor.name,
    contacts: pet.tutor.contacts
      .filter((contact) => contact.public)
      .map(({ kind, value, label }) => ({ kind, value, label }))
  }
});

export const toScanHistoryDto = (scan) => ({ ...scan });
