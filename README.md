# CadeMeuPet

Backend scaffold with tenant isolation for tutor-owned data.

## Multi-tenant decision

The tenant is the tutor/user that owns pets and related operational data. The current tenant is resolved from JWT claims in this order:

1. `tenant_id`
2. `tutor_id`
3. `ClaimTypes.NameIdentifier`
4. `sub`

Tenant-scoped entities implement `ITenantScopedEntity` and receive a required `TenantId`. `ApplicationDbContext` applies Entity Framework global query filters and stamps newly added entities with the current tenant during `SaveChanges`.
