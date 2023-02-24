<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['Ventas_idVentas']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * FROM detalleventas  WHERE Ventas_idVentas=:Ventas_idVentas");
      $sql->bindValue(':Ventas_idVentas', $_GET['Ventas_idVentas']);
	  
      $sql->execute();
      $sql->setFetchMode(PDO::FETCH_ASSOC);
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
      }


else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM detalleventas");
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
    $sql = "INSERT INTO detalleventas
	       (idDetalleVentas, Productos_idProductos, Ventas_idVentas,cantidad,precio_venta)
		    VALUES
           (:idDetalleVentas, :Productos_idProductos, :Ventas_idVentas,:cantidad,:precio_venta)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postidDetalleVentas = $dbConn->lastInsertId();
    if($postidDetalleVentas)
    {
      $input['idDetalleVentas'] = $postidDetalleVentas;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $idDetalleVentas = $_GET['idDetalleVentas'];
  $statement = $dbConn->prepare("DELETE FROM  detalleventas where idDetalleVentas=:idDetalleVentas");
  $statement->bindValue(':idDetalleVentas', $idDetalleVentas);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postidDetalleVentas = $input['idDetalleVentas'];
    $fields = getParams($input);

    $sql = "
          UPDATE detalleventas
          SET $fields
          WHERE idDetalleVentas='$postidDetalleVentas'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>