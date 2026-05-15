import { randomUUID } from 'node:crypto';

const publicCode = () => randomUUID().replaceAll('-', '').slice(0, 12);

export class PetRepository {
  #pets = new Map();
  #scans = [];

  list() {
    return [...this.#pets.values()];
  }

  findById(id) {
    return this.#pets.get(id);
  }

  findByPublicCode(code) {
    return this.list().find((pet) => pet.publicCode === code);
  }

  create(input) {
    const now = new Date().toISOString();
    const pet = {
      id: randomUUID(),
      publicCode: input.publicCode ?? publicCode(),
      name: input.name,
      species: input.species,
      breed: input.breed,
      color: input.color,
      description: input.description,
      photoUrl: input.photoUrl,
      status: input.status,
      medicalNotes: input.medicalNotes,
      privateNotes: input.privateNotes,
      tutor: {
        id: input.tutor.id ?? randomUUID(),
        name: input.tutor.name,
        document: input.tutor.document,
        email: input.tutor.email,
        phone: input.tutor.phone,
        contacts: input.tutor.contacts ?? []
      },
      createdAt: now,
      updatedAt: now
    };

    this.#ensureUniquePublicCode(pet.publicCode);
    this.#pets.set(pet.id, pet);
    return pet;
  }

  update(id, input) {
    const current = this.findById(id);
    if (!current) return undefined;

    if (input.publicCode && input.publicCode !== current.publicCode) {
      this.#ensureUniquePublicCode(input.publicCode);
    }

    const tutor = input.tutor
      ? {
          ...current.tutor,
          ...input.tutor,
          id: input.tutor.id ?? current.tutor.id,
          contacts: input.tutor.contacts ?? current.tutor.contacts
        }
      : current.tutor;

    const updated = {
      ...current,
      ...input,
      tutor,
      updatedAt: new Date().toISOString()
    };

    this.#pets.set(id, updated);
    return updated;
  }

  delete(id) {
    return this.#pets.delete(id);
  }

  listScans(petId) {
    return this.#scans.filter((scan) => scan.petId === petId);
  }

  registerScan(pet, metadata = {}) {
    const scan = {
      id: randomUUID(),
      petId: pet.id,
      publicCode: pet.publicCode,
      scannedAt: new Date().toISOString(),
      ipAddress: metadata.ipAddress,
      userAgent: metadata.userAgent
    };
    this.#scans.push(scan);
    return scan;
  }

  #ensureUniquePublicCode(code) {
    if (this.findByPublicCode(code)) {
      const error = new Error('publicCode already exists');
      error.statusCode = 409;
      throw error;
    }
  }
}
