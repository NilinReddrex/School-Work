<?php
/* References:
isset() https://www.w3schools.com/php/func_var_isset.asp
empty() https://www.w3schools.com/pHP/func_var_empty.asp
is_numeric() https://www.php.net/manual/en/function.is-numeric.php
intval() https://www.php.net/manual/en/function.intval.php
include https://www.w3schools.com/php/php_includes.asp
*/
if(!isset($_GET["number"]) ||
empty($_GET["number"]) || 
!is_numeric($_GET["number"]) ||
 intval($_GET["number"]) <= 0)
{
  $error = "A positive integer is required.";
  include "error.php";
  return;
}

$func =  "Factorial";

foreach ($_GET as $key => $value) {
   $inputs .="{$key} = {$value}";
}


$total = $_GET["number"];
for($i = $_GET["number"] - 1; $i > 0; $i--){
    $total = $total * $i;
}
$result = "The factorial of " . $value . " is " . $total . ".";
include "output.php";
?>