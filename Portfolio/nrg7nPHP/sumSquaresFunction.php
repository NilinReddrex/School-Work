<?php 
/* References:
is_numeric() https://www.php.net/manual/en/function.is-numeric.php
intval() https://www.php.net/manual/en/function.intval.php
include https://www.w3schools.com/php/php_includes.asp
*/

if(is_numeric($_GET["n1"]) === false || is_numeric($_GET["n2"]) === false)
{
    $error = "An integer is required for both n1 and n2.";
    include "error.php";
    return;
}

if(intval($_GET["n1"]) < 1)
{
    $error = "n1 must be 1 or greater.";
    include "error.php";
    return;
}
else if(intval($_GET["n1"]) >= intval($_GET["n2"])){
    $error = "n1 must be less than n2.";
    include "error.php";
    return;
}


$func = "Sum of Squares";



foreach ($_GET as $key => $value) {
    $inputs .= "{$key} = {$value}; ";
}


$output = 0;
for($i = $_GET["n1"]; $i <= $_GET["n2"]; $i++){
    $output += pow($i, 2);
}
$result = "The sum of squares between ". $_GET["n1"] . " and " . $_GET["n2"] . " is " . $output . ".";
include "output.php";


?>