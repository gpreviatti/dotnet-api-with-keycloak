# Dotnet api With Keycloak Authorization

Prof of concept to test dotnet authentication and authorization with keycloak

## References

[Get starting with keycloak](https://www.keycloak.org/docs/latest/authorization_services/index.html#_getting_started_hello_world_create_realm)

[Blob post reference](https://nikiforovall.github.io/aspnetcore/dotnet/2022/08/24/dotnet-keycloak-auth.html)

## Setup new Realm

1. On top of left side of the menu you should select the active realm and click in the button create new realm, then type the new realm name and click on create.

2. Go to clients menu item and click on button `Create client`
    - Cliente type: OpenId Connect
    - Type your Client Id and click in next
    - Set the options `Client authentication` and Authorization to `On` and click to Save button

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

## Bckup database

Inside your postgres container performe the following commands

To backup

``
pg_dump keycloak > /backup/keycloak
``

To Restore

``
psql keycloak < /backup/keycloak
``

[Reference](https://www.postgresql.org/docs/current/backup-dump.html)
