<?php

declare(strict_types=1);

use App\Application\Actions\User\ListUsersAction;
use App\Application\Actions\User\ViewUserAction;
use Psr\Http\Message\ResponseInterface as Response;
use Psr\Http\Message\ServerRequestInterface as Request;
use Slim\App;
use Slim\Interfaces\RouteCollectorProxyInterface as Group;

return function (App $app) {
    $app->options('/{routes:.*}', function (Request $request, Response $response) {
        // CORS Pre-Flight OPTIONS Request Handler
        return $response;
    });

    $app->get('/', function (Request $request, Response $response) {
        $response->getBody()->write('Hello world!');
        return $response;
    });



    /****************** Table users ********************************/

    // selectionner tout les user
    $app->get('/getAllusers', function (Request $request, Response $response) {
        // $app->get('/API/GestionAbsenceAPI/getAllAbsence', function (Request $request, Response $response) {  //test via localhost
        $db = $this->get(PDO::class);
        $sth = $db->prepare("SELECT * FROM `users`");
        $sth->execute();
        $data = $sth->fetchAll(PDO::FETCH_ASSOC);
        $payload = json_encode($data);
        $response->getBody()->write($payload);
        return $response
            ->withHeader('Content-Type', 'application/json');
    });

    // ajouter un users
    $app->post('/users', function (Request $request, Response $response) {
        $data = $request->getParsedBody();
        $db = $this->get(PDO::class);

        $sth = $db->prepare("INSERT INTO users (first_name, last_name, phone, email, password_hash, birth) 
                         VALUES (:first_name, :last_name, :phone, :email, :password_hash, :birth)");

        $sth->bindParam(":first_name", $data['first_name']);
        $sth->bindParam(":last_name", $data['last_name']);
        $sth->bindParam(":phone", $data['phone']);
        $sth->bindParam(":email", $data['email']);
        $sth->bindParam(":password_hash", $data['password_hash']);
        $sth->bindParam(":birth", $data['birth']);

        $sth->execute();

        $responseData = ["message" => "User entry created successfully"];
        $response->getBody()->write(json_encode($responseData));
        return $response
            ->withStatus(201)
            ->withHeader('Content-Type', 'application/json');
    });


    // Supprimer un utilisateur par ID
    $app->delete('/del_users/{id}', function (Request $request, Response $response, array $args) {
        try {
            $id = $args['id'];
            $db = $this->get(PDO::class);
            $sth = $db->prepare("DELETE FROM `users` WHERE `id` = ?");
            $result = $sth->execute([$id]);

            $response->getBody()->write(json_encode($result));
            return $response->withHeader('content-type', 'application/json')->withStatus(200);
        } catch (PDOException $e) {
            $error = array("message" => $e->getMessage());
            $response->getBody()->write(json_encode($error));
            return $response->withHeader('content-type', 'application/json')->withStatus(500);
        }
    });


    // modifier users
    $app->put('/Edit_users/{id}', function (Request $request, Response $response, array $args) {
        $id = $args['id'];
        $data = $request->getParsedBody();
        $db = $this->get(PDO::class);
        $sth = $db->prepare("UPDATE users SET first_name = :first_name, last_name = :last_name, phone = :phone, email = :email, password_hash = :password_hash, birth = :birth WHERE id = :id");
        $sth->bindParam(":id", $id, PDO::PARAM_INT);
        $sth->bindParam(":first_name", $data['first_name']);
        $sth->bindParam(":last_name", $data['last_name']);
        $sth->bindParam(":phone", $data['phone']);
        $sth->bindParam(":email", $data['email']);
        $sth->bindParam(":password_hash", $data['password_hash']);
        $sth->bindParam(":birth", $data['birth']);
        $sth->execute();

        $responseData = ["message" => "User entry updated successfully"];
        $response->getBody()->write(json_encode($responseData));
        return $response
            ->withStatus(200)
            ->withHeader('Content-Type', 'application/json');
    });


    /****************** Table password_entries ********************************/

// sélectionner tous les password_entries
    $app->get('/getAllpassword_entries', function (Request $request, Response $response) {
        $db = $this->get(PDO::class);
        $sth = $db->prepare("SELECT * FROM `password_entries`");
        $sth->execute();
        $data = $sth->fetchAll(PDO::FETCH_ASSOC);
        $payload = json_encode($data);
        $response->getBody()->write($payload);
        return $response
            ->withHeader('Content-Type', 'application/json');
    });

// ajouter un password
    $app->post('/passwords', function (Request $request, Response $response) {
        $data = $request->getParsedBody();
        $db = $this->get(PDO::class);
        $sth = $db->prepare("INSERT INTO password_entries (user_id, site_web, description, password_encrypted, encryption_key, encryption_iv, url_site_web) 
                     VALUES (:user_id, :site_web, :description, :password_encrypted, :encryption_key, :encryption_iv, :url_site_web)");

        $sth->bindParam(":user_id", $data['user_id']);
        $sth->bindParam(":site_web", $data['site_web']);
        $sth->bindParam(":description", $data['description']);
        $sth->bindParam(":password_encrypted", $data['password_encrypted']);  // à chiffrer avant l'insertion
        $sth->bindParam(":encryption_key", $data['encryption_key']);          // clé de déchiffrement chiffrée
        $sth->bindParam(":encryption_iv", $data['encryption_iv']);            // vecteur d'initialisation
        $sth->bindParam(":url_site_web", $data['url_site_web']);

        $sth->execute();

        $responseData = ["message" => "Password entry created successfully"];
        $response->getBody()->write(json_encode($responseData));
        return $response
            ->withStatus(201)
            ->withHeader('Content-Type', 'application/json');
    });

// modifier password
    $app->put('/Edit_passwords/{id}', function (Request $request, Response $response, array $args) {
        $id = $args['id'];
        $data = $request->getParsedBody();
        $db = $this->get(PDO::class);
        $sth = $db->prepare("UPDATE password_entries SET site_web = :site_web, description = :description, password_encrypted = :password_encrypted, encryption_key = :encryption_key, encryption_iv = :encryption_iv, url_site_web = :url_site_web WHERE id = :id");
        $sth->bindParam(":id", $id, PDO::PARAM_INT);
        $sth->bindParam(":site_web", $data['site_web']);
        $sth->bindParam(":description", $data['description']);
        $sth->bindParam(":password_encrypted", $data['password_encrypted']);
        $sth->bindParam(":encryption_key", $data['encryption_key']);
        $sth->bindParam(":encryption_iv", $data['encryption_iv']);
        $sth->bindParam(":url_site_web", $data['url_site_web']);
        $sth->execute();

        $responseData = ["message" => "Password entry updated successfully"];
        $response->getBody()->write(json_encode($responseData));
        return $response
            ->withStatus(200)
            ->withHeader('Content-Type', 'application/json');
    });

// Supprimer un password par ID
    $app->delete('/del_password/{id}', function (Request $request, Response $response, array $args) {
        try {
            $id = $args['id'];
            $db = $this->get(PDO::class);
            $sth = $db->prepare("DELETE FROM `password_entries` WHERE `id` = ?");
            $result = $sth->execute([$id]);

            $response->getBody()->write(json_encode($result));
            return $response->withHeader('content-type', 'application/json')->withStatus(200);
        } catch (PDOException $e) {
            $error = array("message" => $e->getMessage());
            $response->getBody()->write(json_encode($error));
            return $response->withHeader('content-type', 'application/json')->withStatus(500);
        }
    });

};
