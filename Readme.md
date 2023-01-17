# Dotnet api with Keycloak authentication/authorization

Prof of concept to test dotnet authentication and authorization with keycloak

## References

[Get starting with keycloak](https://www.keycloak.org/docs/latest/authorization_services/index.html#_getting_started_hello_world_create_realm)

[Blob post reference](https://nikiforovall.github.io/aspnetcore/dotnet/2022/08/24/dotnet-keycloak-auth.html)

## Run stack

```sh
cd infra && docker-compose up
```

## Backup and restore your configurations

Backup

```sh
cd infra && docker-compose exec keycloak kc.sh export --realm `<your realm name>` --dir /imports
```

Restore

```sh
cd infra && docker-compose exec keycloak kc.sh import --dir /imports
```

[Reference](https://www.keycloak.org/server/importExport#:~:text=To%20export%20a%20realm%2C%20you,started%20when%20invoking%20this%20command.&text=To%20export%20a%20realm%20to,%2D%2Ddir%20option.&text=When%20exporting%20realms%20to%20a,for%20each%20realm%20being%20exported.)


## Default credentials

Keycloak admin user

```
login: admin
password: admin
```

User with admin role

```
login: giovanni
password: Change@Me
```

User without admin role

```
login: joao
password: Change@Me
```

[Setup new realm wiki page](https://github.com/gpreviatti/dotnet-api-with-keycloak/wiki/Setup-new-Realm)