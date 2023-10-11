<?php
// Data base connection
$server = "localhost";
$user = "davidbo5";
$password = "9kPnax8NCqjb";
$dbname = "davidbo5";

$connection = mysqli_connect($server, $user, $password, $dbname);

if (!$connection) {
  die("Error al conectar a la base de datos: " . mysqli_connect_error());
}

if ($_SERVER["REQUEST_METHOD"] === "POST") {
  // ------------------------------------------------------------------------------ USER
  // ---------------------------------------------------- Nombre
    if (isset($_POST["Name"])) {
      $name = $_POST["Name"];
    } else {
    // Manejo de la situacion en la que "name" no esta definido en $_POST
      $name = "Nombre no definido";
    }

    // ---------------------------------------------------- Edad
    if (isset($_POST["Age"])) {
      $age = $_POST["Age"];
    } else {
    // Manejo de la situacion en la que "name" no esta definido en $_POST
      $age = "Edad no definida";
    }

    // ---------------------------------------------------- Genero
    if (isset($_POST["Gender"])) {
      $gender = $_POST["Gender"];
    } else {
    // Manejo de la situacion en la que "name" no esta definido en $_POST
      $gender = "Genero no definido";
    }

    // ---------------------------------------------------- Pais
    if (isset($_POST["Country"])) {
      $country = $_POST["Country"];
    } else {
    // Manejo de la situacion en la que "name" no esta definido en $_POST
      $country = "Country no definido";
    }

    // ---------------------------------------------------- FirstLoginDate
    if (isset($_POST["Date"])) {
      $date = $_POST["Date"];
    } else {
    // Manejo de la situacion en la que "name" no esto definido en $_POST
      $date = "First Login Date no definido";
    }

    // INSERT INTO
    $sql = "INSERT INTO User(Username, Age, Gender, Country, FirstLoginDate) VALUES ('$name', '$age', '$gender', '$country', '$date')";

    if ($connection->query($sql) === TRUE) {
      // Obtiene la última ID generada
      $lastUserID = $connection->insert_id;
  
      // Imprime la última ID generada
      echo $lastUserID;
    } else {
      // Manejar errores de inserción
      echo "Error al crear el registro: " . $connection->error;
    }
    

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------ SESSION

    if (isset($_POST["startSessionTime"])) {
      $startSessionTime = $_POST["startSessionTime"];
    } else {
    // Manejo de la situacion en la que "time" no esto definido en $_POST
      $startSessionTime = "Start Session Time no definido";
    }

    // INSERT INTO
    $sql = "INSERT INTO `Sessions` (`Start`) VALUES ('$startSessionTime')";

    // if ($connection->query($sql) === TRUE) {
    //   // Obtiene la última ID generada
    //   $lastSessionID = $connection->insert_id;
  
    //   // Imprime la última ID generada
    //   //echo $lastSessionID;
    // } else {
    //   // Manejar errores de inserción
    //   echo "Error al crear el registro: " . $connection->error;
    // }
    // ------------------------------------------------------------------------------

    
    // CLOSE
    mysqli_close($connection);
}
?>
