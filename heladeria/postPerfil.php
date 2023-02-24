<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idPerfil']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * perfil  where idPerfil=:idPerfil");
      $sql->bindValue(':idPerfil', $_GET['idPerfil']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
      }


else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM perfil");
      $sql->execute();
      $sql->setFetchMode(PDO::FETCH_ASSOC);
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
    }
}
if ($_SERVER['REQUEST_METHOD'] == 'POST')
{
    $input = $_POST;
    $sql = "INSERT INTO perfil
          (idPerfil, nombre, descripcion)
          VALUES
          (:idPerfil, :nombre, :descripcion)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postidPerfil = $dbConn->lastInsertId();
    if($postidPerfil)
    {
      $input['idPerfil'] = $postidPerfil;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $idPerfil = $_GET['idPerfil'];
  $statement = $dbConn->prepare("DELETE FROM  perfil where idPerfil=:idPerfil");
  $statement->bindValue(':idPerfil', $idPerfil);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postidPerfil = $input['idPerfil'];
    $fields = getParams($input);

    $sql = "
          UPDATE perfil
          SET $fields
          WHERE idPerfil='$postidPerfil'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>