<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['usuario']) and isset($_GET['contrasena'])  )
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * FROM usuario  where usuario=:usuario and contrasena =:contrasena");
      $sql->bindValue(':usuario', $_GET['usuario']);
	  $sql->bindValue(':contrasena', $_GET['contrasena']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
      }


else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM usuario");
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
    $sql = "INSERT INTO usuario
          (idUsuario, cedula, nombres, apellidos, telefono, direccion, correo, usuario, contrasena, estado, imagen, Perfil_idPerfil)
          VALUES
          (:idUsuario, :cedula, :nombres, :apellidos, :telefono, :direccion, :correo, :usuario, :contrasena, :estado, :imagen, :Perfil_idPerfil)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postidUsuario = $dbConn->lastInsertId();
    if($postidUsuario)
    {
      $input['idUsuario'] = $postidUsuario;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $idUsuario = $_GET['idUsuario'];
  $statement = $dbConn->prepare("DELETE FROM  usuario where idUsuario=:idUsuario");
  $statement->bindValue(':idUsuario', $idUsuario);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postidUsuario = $input['idUsuario'];
    $fields = getParams($input);

    $sql = "
          UPDATE usuario
          SET $fields
          WHERE idUsuario='$postidUsuario'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>