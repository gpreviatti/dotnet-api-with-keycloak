# Dotnet api With Keycloak Authorization

Prof of concept to test dotnet authentication and authorization with keycloak

## References

[Get starting with keycloak](https://www.keycloak.org/docs/latest/authorization_services/index.html#_getting_started_hello_world_create_realm)

[Blob post reference](https://nikiforovall.github.io/aspnetcore/dotnet/2022/08/24/dotnet-keycloak-auth.html)

## Run stack

``sh
cd infra && docker-compose up
``

## Backup and restore your configurations

Backup

``sh
cd infra && docker-compose exec keycloak kc.sh export --realm `<your realm name>` --dir /imports
``

Restore

``sh
cd infra && docker-compose exec keycloak kc.sh import --dir /imports
``

[Reference](https://www.keycloak.org/server/importExport#:~:text=To%20export%20a%20realm%2C%20you,started%20when%20invoking%20this%20command.&text=To%20export%20a%20realm%20to,%2D%2Ddir%20option.&text=When%20exporting%20realms%20to%20a,for%20each%20realm%20being%20exported.)


<details>
    <summary>Setup new Realm</sumary>

1. On top of left side of the menu you should select the active realm and click in the button `create new realm`, then type the new realm name and click on `create`.

2. Go to clients menu item and click on button `Create client`
    - Cliente type: OpenId Connect
    - Type your Client Id and click in next
    - Set the options `Client authentication` and `Authorization` to `ON` and click to Save button

3. Go to Client scopes menu item and clic on button `Create client scope`
    - Type the name of your client scope
    - Type: Default
    - Protocol: OpenId Connect
    - Click to save
    - Go to Mappers and click in the button `Configure a new mapper`
    - Choose `Audience` mapper create a name, select your client and save

4. In the Realm roles menu create a new role, in this example we will call `admin`

5. Go to Users menu and create a new user
    - Type username, First Name and Last Name and click in Create button
    - Go to Credentials menu and set up a new password the user. Unflag Temporary password
    - Assaign `admin` realm role on Role mapping menu

6. By the end go back to Clients menu and in `Client scopes` add your client scope to your client as a Default

7. To get your client configuration to use on your application, go to `Clients` select your client and in Action button in the top o right side select the option `Download adapter config`, than copy this information in your appsettings file
</details>


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