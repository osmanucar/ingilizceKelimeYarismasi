<?php
$baglanti = new mysqli("localhost","root","","learnwords_db"); 

// Check connection veritabanı bağlantısı
if ($baglanti -> connect_errno) {
  echo "Failed to connect to MySQL: " . $baglanti -> connect_error;
  exit();
}

if($_POST['unity']=="girisYapma"){
	//veritabanındaki kullanıcıları okuyup giriş yapmamıza yarar.
	$kullaniciAdi=$_POST['kullaniciAdi'];
	$sifre=$_POST['sifre'];
	
	$sorgu="select * from kullanicilar where kullaniciAdi='$kullaniciAdi' and sifre='$sifre'";
	
	$sorguSonucu=$baglanti -> query($sorgu);
	
	if($sorguSonucu->num_rows>0){
		echo "Giriş Başarılı";
	}else{
		echo "Kullanıcı Adı veya Şifre Hatalı";
	}

}

if($_POST['unity']=="kayitOl"){	
	//veritabanına kullanıcı kaydı yapma
	$ad=$_POST['ad'];
	$soyad=$_POST['soyad'];
	$kullaniciAdi=$_POST['kullaniciAdi'];
	$sifre=$_POST['sifre'];	
	
	$sorgu="insert into kullanicilar (ad,soyad,kullaniciAdi,sifre,skor) Values ('$ad','$soyad','$kullaniciAdi','$sifre',0)";

    $sorguSonucu=$baglanti -> query($sorgu);
	
	if($sorguSonucu){
		echo"Kaydınız başarıyla gerçekleştirildi";
	}else{
		echo"Bu kullanıcı adı alınmış farkı bir kullanıcı adı oluşturunuz ";
	}	
}

if($_POST['unity']=="sorular"){
	
	$sorgu="select * from sorular";
    $sorguSonucu=$baglanti -> query($sorgu);

if($sorguSonucu->num_rows>0){
	$butunSatirlar=array();
    while($satirlar=$sorguSonucu->fetch_object()){

    array_push($butunSatirlar,array(
	
	"soru"=>$satirlar->soru,
	"a_Cevab"=>$satirlar->a_Cevabi,
	"b_Cevab"=>$satirlar->b_Cevabi,
	"c_Cevab"=>$satirlar->c_Cavabi,
	"d_Cevab"=>$satirlar->d_Cevabi,
	"dogruCevab"=>$satirlar->dogruCevap,
	"saniye"=>$satirlar->saniye,	
	)
	);

    }
    echo json_encode(array("butunSorular"=>$butunSatirlar));
    }
    else{
        //Hata
    }
}

//////////////
if($_POST['unity']=="turkceSorulari"){
	
	$sorgu="select * from turkceSorular";
    $sorguSonucu=$baglanti -> query($sorgu);

if($sorguSonucu->num_rows>0){
	$butunSatirlar=array();
    while($satirlar=$sorguSonucu->fetch_object()){

    array_push($butunSatirlar,array(
	
	"soru"=>$satirlar->soru,
	"a_Cevab"=>$satirlar->a_Cevabi,
	"b_Cevab"=>$satirlar->b_Cevabi,
	"c_Cevab"=>$satirlar->c_Cevabi,
	"d_Cevab"=>$satirlar->d_Cevabi,
	"dogruCevab"=>$satirlar->dogruCevap,
	"saniye"=>$satirlar->saniye,	
	)
	);

    }
    echo json_encode(array("butunSorular"=>$butunSatirlar));
    }
    else{
        //Hata
    }
}

$baglanti->close();
?>