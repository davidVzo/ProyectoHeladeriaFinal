<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['idProductos']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * productos  where idProductos=:idProductos");
      $sql->bindValue(':idProductos', $_GET['idProductos']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
      }


else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM productos");
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
    $sql = "INSERT INTO productos
	       (idProductos, nombreProducto, costo, adereso, sabor, imagen)
		    VALUES
           (:idProductos, :nombreProducto, :costo, :adereso, :sabor, :imagen)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postidProductos = $dbConn->lastInsertId();
    if($postidProductos)
    {
      $input['idProductos'] = $postidProductos;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
     }
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
    $idProductos = $_GET['idProductos'];
  $statement = $dbConn->prepare("DELETE FROM  productos where idProductos=:idProductos");
  $statement->bindValue(':idProductos', $idProductos);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postidProductos = $input['idProductos'];
    $fields = getParams($input);

    $sql = "
          UPDATE productos
          SET $fields
          WHERE idProductos='$postidProductos'
           ";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
?>