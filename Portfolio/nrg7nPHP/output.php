<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <title>Output</title>
          <!-- Bootstrap CSS -->
          <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
          <!-- external css -->
          <link rel="stylesheet" type="text/css" href="index.css">
          <link rel="stylesheet" type="text/css" href="output.css">
    </head>
    <body>
        <div id="formWrapper" class="container-fluid">
            <h1 id="title">Results</h1>
            <hr>
            <p>The selected function is: <?php echo "$func" ?></p>
            <p>Input: <?php echo "$inputs" ?></p>
            <p>Output: <?php echo "$result" ?></p>
        </div>
    </body>
</html>