<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idVentas']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * FROM ventas  where idVentas=:idVentas");
      $sql->bindValue(':idVentas', $_GET['idVentas']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
      }


else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM ventas");
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
    $sql = "INSERT INTO ventas
	       (idVentas, numeroVenta, fecha, precioTotal, Usuario_idUsuario, Clientes_idUsuario)
		    VALUES
           (:idVentas, :numeroVenta, :fecha, :precioTotal, :Usuario_idUsuario, :Clientes_idUsuario)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postidVentas = $dbConn->lastInsertId();
    if($postidVentas)
    {
      $input['idVentas'] = $postidVentas;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $idVentas = $_GET['idVentas'];
  $statement = $dbConn->prepare("DELETE FROM  ventas where idVentas=:idVentas");
  $statement->bindValue(':idVentas', $idVentas);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postidVentas = $input['idVentas'];
    $fields = getParams($input);

    $sql = "
          UPDATE ventas
          SET $fields
          WHERE idVentas='$postidVentas'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>