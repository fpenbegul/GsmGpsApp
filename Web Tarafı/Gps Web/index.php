<!DOCTYPE html>
<html lang="en">
<head>
<link rel="icon" type="image/png" href="DU.png" />
  <title>Akçakoca Myo</title>
  	<meta name="theme-color" content="#008BDE"/>
  <meta charset="utf-8">
  <script src="jquery-1.12.3.min.js"></script>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
</head>
<body>
<?php date_default_timezone_set('Europe/Istanbul'); ?>
<div class="container"><br><br>
  <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#home"><img src="67347.svg" width="20" height="20">Konumumu Kaydet</a></li>
    <li><a data-toggle="tab" href="#menu1"><img src="detail.png" width="20" height="20">  Detayları Göster</a></li>
    <li><a data-toggle="tab" href="#menu2"><img src="image.png" width="20" height="20">  Resim Üzerinde Göster</a></li>
  </ul>

  <div class="tab-content">
    <div align="left"id="home" class="tab-pane fade in active">
      <h3>Konum Kaydedici</h3>
      İsminizi Girdikten Sonra Konumunuzu Kaydetmek İçin Butona Tıklayın.<br><br>
 <strong> İsminiz:</strong>
  <input type="text" name="metin"/>  <button class="btn btn-primary btn-sm" onclick="getLocation()">Konumumu Kaydet</button><br><br>
  <div id="kayitedildi"></div>
  
<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>     <pre align="center"> Akçakoca Meslek Yüksek Okulu - Bilgisayar Programcılığı 2016 </pre>
<div class="container"></div>
    </div>
    <div id="menu1" class="tab-pane fade">
      <h3>Konum, Zaman ve Diğer Detaylar</h3>
      <p id="y"></p>
      <p id="j">
      <p id="x">Henüz konum kaydı yapılmadığından Detaylar Gösterilemiyor.</p></p>
      <pre align="center"> Akçakoca Meslek Yüksek Okulu - Bilgisayar Programcılığı 2016 </pre>
    </div>
    <div id="menu2" class="tab-pane fade">
      <h3>Resim Üzerinde Konumuz</h3>
	<p id="sonuc">Henüz konum kaydı yapılmadığından konumuz Resim Üzerinde Gösterilemiyor.</p>
          <pre align="center"> Akçakoca Meslek Yüksek Okulu - Bilgisayar Programcılığı 2016 </pre>
    </div>
  </div>
</div>
<script>
    var x = document.getElementById("x");
    var y = document.getElementById("y");
	var j=document.getElementById("j");
	var p=document.getElementById('kayitedildi');

    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition); //watchPosition yerine getCurrentPosition
        } else { 
            x.innerHTML = "Geolocation bu tarayıcıda Desteklenmemektedir!";
        }
    }
	

    function showPosition(position) {

        var lon= position.coords.longitude;
        var lat= position.coords.latitude;
    	var speed= position.coords.speed;
		var latlng=position.coords.latitude+","+position.coords.longitude;
        var metin = $("[name=metin]").val();
		var gpstekisaat=position.timestamp;
		var yukseklik=position.altitude;
		//x.innerHTML=gpstekisaat;
		x.innerHTML="";
		x.innerHTML=x.innerHTML+'<pre><strong>Kayıt Edilen İsim: </strong>'+metin+'<br>';
		x.innerHTML=x.innerHTML+'<pre><strong>Enleminiz:</strong> '+lat+'<br></pre>';
		x.innerHTML=x.innerHTML+'<pre><strong>Boylamınız:</strong> '+lon+'<br></pre>';
		x.innerHTML=x.innerHTML+'<pre><strong>Hızınız:</strong> '+speed+'<br></pre>';
		x.innerHTML=x.innerHTML+'<pre><strong>Yüksekliğiniz:</strong> '+yukseklik+'<br></pre>';
	    x.innerHTML=x.innerHTML+'<pre><strong>Saat:</strong> <?php $hourMin = date('H:i:s');echo $hourMin;?><br></pre>';
		x.innerHTML=x.innerHTML+'<pre><strong>Tarih:</strong> <?php $maydaymayday=date('d.m.Y');echo $maydaymayday;?><br></pre>';
		y.className+='alert alert-info fade in';
		y.innerHTML='<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong> Bilgi! </strong> Alınan Veriye Göre Hız ve Yükseklik Parametreleri Değer Döndürmeyebilir!';
        $.ajax({
                type: 'POST',
                url: 'ajax.php',
                data:  { lat:lat, lon:lon, speed:speed, metin:metin, latlng:latlng},
                success: function(result) {
				p.className+='alert alert-success fade in';
				p.innerHTML='<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong> Başarılı! </strong>'+'Konumunuz Kaydedildi. Lütfen Diğer sayfalardan Kontrol Ediniz.';
                    $('#sonuc').html(result);
                },
                error: function() {
                   // alert('Gönderilemedi');
                }
        });
		

    }
</script>
</body>
</html>
