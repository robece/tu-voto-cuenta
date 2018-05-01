
	<script type="text/javascript" src="//maps.google.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
function initialize()
{
	var mapDiv = document.getElementById('map');
	var map = new google.maps.Map(mapDiv, {
		center: new google.maps.LatLng(48.856614, 2.352222),
		zoom: 13,
		mapTypeId: google.maps.MapTypeId.ROADMAP,
		scrollwheel: false
	});

	new google.maps.Marker({
		position: new google.maps.LatLng(48.856614, 2.352222),
		map: map
	});
}

google.maps.event.addDomListener(window, 'load', initialize);
</script>

<section class="contact-map" id="map"></section>


<section class="contact-container">

	<div class="container">

		<div class="row">

			<div class="col-sm-7 sep">

				<h4>Póngase en contacto con nosotros!</h4>

				<p>
					texto de prueba <br/>
				</p>

				<form class="contact-form" role="form" method="post" action="" enctype="application/x-www-form-urlencoded">

					<div class="form-group">
						<input type="text" name="name" class="form-control" placeholder="Nombre:" />
					</div>

					<div class="form-group">
						<input type="text" name="email" class="form-control" placeholder="E-mail:" />
					</div>

					<div class="form-group">
						<textarea class="form-control" name="message" placeholder="Mensaje:" rows="6"></textarea>
					</div>

					<div class="form-group text-right">
						<button class="btn btn-primary" name="send">Enviar</button>
					</div>

				</form>

			</div>

		</div>

	</div>

</section>
