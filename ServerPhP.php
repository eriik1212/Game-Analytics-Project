<?php
echo "This is your data!" . "<br>";

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

    echo "Name: " . htmlspecialchars($name) . "<br>";
    echo "Age: " . htmlspecialchars($age) . "<br>";
    echo "Gender: " . htmlspecialchars($gender) . "<br>";
    echo "Country: " . htmlspecialchars($country) . "<br>";
    echo "Date: " . htmlspecialchars($date);
    
    // INSERT INTO
    $sql = "INSERT INTO User(Username, Age, Gender, Country, FirstLoginDate) VALUES ('$name', '$age', '$gender', '$country', '$date')";


    if (mysqli_query($connection, $sql)) {
       echo "Datos insertados correctamente.";
    } else {
       echo "Error al insertar datos: " . mysqli_error($connection);
    }
    
    // CLOSE
    mysqli_close($connection);
}

else {
  echo "<pre>";
var_dump($_POST);
echo "</pre>";

}
?>
