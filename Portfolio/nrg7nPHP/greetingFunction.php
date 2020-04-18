<?php
/* References:
isset() https://www.w3schools.com/php/func_var_isset.asp
empty() https://www.w3schools.com/pHP/func_var_empty.asp
include https://www.w3schools.com/php/php_includes.asp
*/
if(!isset($_POST["fname"]) ||!isset($_POST["lname"]) ||
empty($_POST["fname"]) || empty($_POST["lname"]))
{
  $error = "Values for fname and lname are required.";
  include "error.php";
  return;
}

$func = "Greeting";

foreach ($_POST as $key => $value) {
    $inputs .= "{$key} = {$value}; ";
}

$result = "Hello " . $_POST["fname"] . " " . $_POST["lname"] . "!";

include "output.php";
?>