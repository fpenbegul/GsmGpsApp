<pre>
<?php 

	if(isset($_POST))
	{

	//VERITABANI AYARLARI
		$DBHOST = "sql8.freesqldatabase.com";
		$DBNAME = "sql8117714";
		$DBPASS = "La3eCAyfNF";
		$DBUSER = "sql8117714";

		//TARIH ZAMAN BÖLGESİ
		date_default_timezone_set('Europe/Istanbul');

		//PDO BAĞLANTISINI YAPALIM
		try 
		{
			$db = new PDO("mysql:host=".$DBHOST."; dbname=".$DBNAME."; charset=utf8", $DBUSER , $DBPASS );
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE,PDO::FETCH_OBJ);
			$db->exec("SET NAMES UTF8");
		}
		catch ( PDOException $e ){
			print $e->getMessage();
		}

		//GELEN POST BİLGİLERİNİ DEĞİŞKENE ATADIK
		$lat 	= $_POST['lat'];
		$lon 	= $_POST['lon'];
		$speed 	= $_POST['speed'];
		$text 	= $_POST['metin'];
		$latlng = $_POST['latlng'];
		
$geo = 'http://maps.google.com/maps/api/geocode/xml?latlng='.htmlentities(htmlspecialchars(strip_tags($_POST['latlng']))).'&sensor=true';
$xml = simplexml_load_file($geo);


list($lat,$long) = explode(',',htmlentities(htmlspecialchars(strip_tags($_POST['latlng']))));
$adres = $xml->result->formatted_address;
$apiadresi = $geo;

$resimhtml= '<img src="http://maps.google.com/maps/api/staticmap?center='.$lat.','.$lon.'&zoom=15&markers=size:big%7Ccolor:blue%7Clabel:X%7C'.$lat.','.$lon.'&size=750x750&scale=2&format:=png&maptype=roadmap&&sensor=true" width="300px" height="300px" alt="'.$adres.'" \/><br /><br />';
$haritaresim= 'http://maps.google.com/maps/api/staticmap?center='.$lat.','.$lon.'&zoom=15&markers=size:big%7Ccolor:blue%7Clabel:X%7C'.$lat.','.$lon.'&size=750x750&scale=2&format:=png&maptype=roadmap&&sensor=true';
echo '<pre align="center">'.$resimhtml.'</br>';
echo '<b><a href="'.$haritaresim.'">Resmi Büyütmek için Tıklayın</a></b></br></br></pre></br>';


echo '<pre><strong>Api adresi:</strong> '.$apiadresi.'<br>';
echo '<strong>Açık Adresiniz:</strong> '.$adres.'<br></pre>';
		
		
		//DEĞİŞKENLERİ EKRANA BASALIM
		//echo $lat."\n";
		//echo $lon."\n";
		//echo $speed."\n";
		//echo $text."\n";
		//GEÇERLİ SAATİ ALDIK Saat:Dakika:Saniye
		$hourMin = date('H:i:s');
		
		
		//GEÇERLİ TARİHİ ALDIK gün.ay.YIL
		$maydaymayday=date('d.m.Y');


		// SQL SORGUSU
		$sql= "INSERT INTO tablo (ad,latitude,longitude,speed,hour,day,adres,resimyol) VALUES ('{$text}','{$lat}','{$lon}','{$speed}','{$hourMin}','{$maydaymayday}','{$adres}','{$haritaresim}')";

		//SORGUYU ÇALIŞTIRALIM
		$ekle = $db->query($sql);

		//SORGUNUN KONTROLÜNÜ EKRANA BASALIM
		if($ekle){
			echo '<pre><p align="center">Veritabanına Eklendi</p></pre>';
		}else{
			echo "Eklenemedi.";
		}

		//VERITABANINI KAPATALIM
		unset($db);

		//POSTU TEMİZLEYELİM
		unset($_POST);
	}
?>
</pre>