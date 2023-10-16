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

//$lastUserID = null; // Inicializa la variable fuera de los bloques if

if ($_SERVER["REQUEST_METHOD"] === "POST") {
  // ------------------------------------------------------------------------------ USER
    if (isset($_POST["Name"]) && isset($_POST["Age"]) && isset($_POST["Gender"]) && isset($_POST["Country"]) && isset($_POST["Date"])) {

      $name = $_POST["Name"];
      $age = $_POST["Age"];
      $gender = $_POST["Gender"];
      $country = $_POST["Country"];
      $date = $_POST["Date"];

      // INSERT INTO
      $sql = "INSERT INTO User (Username, Age, Gender, Country, FirstLoginDate) VALUES ('$name', '$age', '$gender', '$country', '$date')";

      if ($connection->query($sql) === TRUE) {
        // Obtiene la última ID generada
        $lastUserID = $connection->insert_id;
    
        // Imprime la última ID generada
        echo $lastUserID;
      } else {
        // Manejar errores de inserción
        echo "Error al crear el registro: " . $connection->error;
      }

    } 

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------ SESSION

    else if (isset($_POST["startSessionTime"]) && isset($_POST["endSessionTime"])) {
      $startSessionTime = $_POST["startSessionTime"];
      $endSessionTime = $_POST["endSessionTime"];
    
      // INSERT INTO
      $sql = "INSERT INTO `Sessions`(`Start`, `End`, user_id) VALUES ('$startSessionTime', '$endSessionTime' , 3)";

      echo "Form2 valido";
    if ($connection->query($sql) === TRUE) {
      // Obtiene la ultima ID generada
      $lastSessionID = $connection->insert_id;
  
      // Imprime la ultima ID generada
      //echo $lastSessionID;
    } else {
      // Manejar errores de insercion
      echo "Error al crear el registro: " . $connection->error;
    }
    // ------------------------------------------------------------------------------
  }
  else 
  {
    echo "Form no valido";
  }
    // CLOSE
    mysqli_close($connection);
}
?>
