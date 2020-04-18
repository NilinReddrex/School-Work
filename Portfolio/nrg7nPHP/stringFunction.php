<?php
/* References:
isset() https://www.w3schools.com/php/func_var_isset.asp
empty() https://www.w3schools.com/pHP/func_var_empty.asp
include https://www.w3schools.com/php/php_includes.asp
*/
if(!isset($_GET["string"]) ||
empty($_GET["string"]))
{
    $error = "String is required.";
    include "error.php";
    return;
}

$func = "String Function";

foreach ($_GET as $key => $value) {
    $inputs .= "{$key} = {$value}";
}

/* References:
strrev() https://www.php.net/manual/en/function.strrev.php
*/
$output =strrev($_GET["string"]);

$result = "The reversed of " . $value . " is " . $output . ".";

include "output.php";
?>