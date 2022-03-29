<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>ESG PARTNER KARBON AYAKIZI HESAPLAMA </title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
    <style>
        .display-none{
            display: none;
        }

        input[type="checkbox"]{
            margin-right: 10px;
        }
        .odaperform{
            margin-left: 30px;
        }
        .radiotext{
            padding-left: 5px;
        }
        .radiocolumn{
            padding-top: 10px;
        }
    </style>

    
    <?php
        function strtonumber( $str )
        {
            $number = (float) str_replace(',', '.', str_replace('.', '', $str));
            if( $number == (int) $number ) {
                return (int) $number;
            } else {
                return $number;
            }
        }
        if ($_SERVER["REQUEST_METHOD"] == "POST") {
            // collect value of input field
            $tdgaz = strtonumber($_POST['tdgaz']);
            $tkom = strtonumber($_POST['tkom']);
            $tjmot = strtonumber($_POST['tjmot']);
            $tamot = strtonumber($_POST['tamot']);
            $taben = strtonumber($_POST['taben']);
            $talpg = strtonumber($_POST['talpg']);
            $telk = strtonumber($_POST['telk']);
        }

        function writeMsg() {
            global $tdgaz, $tjmot, $tamot, $taben, $talpg, $telk ;
            $k1dgaz = array( 0.75, 48, 56.1, 0.005, 0.0001 );
            $k1jmot = array( 0.85, 43, 74.1, 0.01, 0.0006);
            $k1amot = array( 0.85, 43, 74.1, 0.0039, 0.0039);
            $k1aben = array( 0.78, 44.3, 69.3, 0.025, 0.008);
            $k1alpg = array( 0.56, 47.3, 63.1, 0.062, 0.0002);

            if (empty($tdgaz) && empty($tkom) && empty($tjmot) && empty($tamot) && empty($taben) && empty($talpg) && empty($telk)) {
                echo "";
            } else {
                $tdgazton = $tdgaz * $k1dgaz[0] / 1000 ;
                $tjmotton = $tjmot * $k1jmot[0] / 1000 ;
                $tamotton = $tamot * $k1amot[0] / 1000 ;
                $tabenton = $taben * $k1aben[0] / 1000 ;
                $talpgton = $talpg * $k1alpg[0] / 1000 ;
                $Emisyon1 = ($tdgazton * $k1dgaz[1] * $k1dgaz[2] / 1000 )+($tdgazton * $k1dgaz[1] * $k1dgaz[3] /1000 * 28 )+($tdgazton * $k1dgaz[1] * $k1dgaz[4] / 1000 * 265 );
                $Emisyon2 = ($tkom * 2);
                $Emisyon3 = ($tjmotton * $k1jmot[1] * $k1jmot[2] / 1000 )+($tjmotton * $k1jmot[1] * $k1jmot[3] /1000 * 28 )+($tjmotton * $k1jmot[1] * $k1jmot[4] / 1000 * 265 );
                $Emisyon4 = ($tamotton * $k1amot[1] * $k1amot[2] / 1000 )+($tamotton * $k1amot[1] * $k1amot[3] /1000 * 28 )+($tamotton * $k1amot[1] * $k1amot[4] / 1000 * 265 );
                $Emisyon5 = ($tabenton * $k1aben[1] * $k1aben[2] / 1000 )+($tabenton * $k1aben[1] * $k1aben[3] /1000 * 28 )+($tabenton * $k1aben[1] * $k1aben[4] / 1000 * 265 );
                $Emisyon6 = ($talpgton * $k1alpg[1] * $k1alpg[2] / 1000 )+($talpgton * $k1alpg[1] * $k1alpg[3] /1000 * 28 )+($talpgton * $k1alpg[1] * $k1alpg[4] / 1000 * 265 );
                $Emisyon7 = ($telk * 0.00045);
                $Emisyon = $Emisyon1 + $Emisyon2 + $Emisyon3 + $Emisyon4 + $Emisyon5 + $Emisyon6 + $Emisyon7;
                
                echo "Tesisin Emisyonu <strong>";
                echo number_format($Emisyon, 2, ",", ".");
                echo "</strong> CO<sub>2</sub>(e) olarak hesaplanmıştır. ";
            }
        }
    ?>
 <body>
   <div style="margin: 30px 0">
       <div class="container">
           <div class="text-center">
                <a href="/"><img alt="esg logo" src="../library/img/logo-footer.png"/></a>
               <hr>
                   <h5>KARBON AYAKİZİ HESAPLAMA FORMU </h5>
               <hr>
           </div>
        <form method="post" action="<?php echo $_SERVER['PHP_SELF'];?>">
            <div class="text-center">
                <h5>KAPSAM 1</h5>
            </div>
            <table>
                <tr>
                    <td>Tesisin tükettiği <strong>doğalgaz</strong> miktarı (m<sup>3</sup>): </td><td><input type="text" name="tdgaz" <?php echo "value=" . number_format($tdgaz, 2, ',', '.');?>></td>
                </tr>
                <tr>
                    <td>Tesisin tükettiği <strong>kömür</strong> miktarı (ton): </td><td><input type="text" name="tkom" <?php echo "value=" . number_format($tkom, 2, ',', '.');?>></td>
                </tr>
                <tr>
                    <td colspan=2><hr></td>
                </tr>
                <tr>
                    <td>Tesisin jenaratörde kullandığı <strong>motorin</strong> miktarı (lt): </td><td><input type="text" name="tjmot" <?php echo "value=" . number_format($tjmot, 2, ',', '.');?>></td>
                </tr>
                <tr>
                    <td colspan=2><hr></td> 
                </tr>
                <tr>
                    <td>Tesisin araçlarında kullandığı <strong>motorin</strong> miktarı (lt): </td><td><input type="text" name="tamot" <?php echo "value=" . number_format($tamot, 2, ',', '.');?>> </td>
                </tr>
                <tr>
                    <td>Tesisin araçlarında kullandığı <strong>benzin</strong> miktarı (lt): </td><td><input type="text" name="taben" <?php echo "value=" . number_format($taben, 2, ',', '.');?>> </td>
                </tr>
                <tr>
                    <td>Tesisin araçlarında kullandığı <strong>LPG</strong> miktarı (lt): </td><td><input type="text" name="talpg" <?php echo "value=" . number_format($talpg, 2, ',', '.');?>> </td>
                </tr>
                <tr>
                    <td colspan=2><hr></td>
                </tr>
            </table>
            <div class="text-center">
                <h5>KAPSAM 2</h5>
            </div>
            <table>
                <tr>
                    <td>Tesisin tükettiği <strong>Elektrik</strong> miktarı (kWh): </td><td><input type="text" name="telk" <?php echo "value=" . number_format($telk, 2, ',', '.');?>></td>
                </tr>
            </table>
            <div class="text-center">
              <input value="HESAPLA" type="submit">
            </div>
        </form>
     </div>
     <hr>
     <div class="text-center">

     <?php

        writeMsg();

      ?>




 </body>
</html>
