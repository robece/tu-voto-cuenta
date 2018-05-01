
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

				<h4>Get in touch with us, write us an e-mail!</h4>

				<p>
					To shewing another demands to. Marianne property cheerful informed at striking at. <br />
					Clothes parlors however by cottage on.
				</p>

				<form class="contact-form" role="form" method="post" action="" enctype="application/x-www-form-urlencoded">

					<div class="form-group">
						<input type="text" name="name" class="form-control" placeholder="Name:" />
					</div>

					<div class="form-group">
						<input type="text" name="email" class="form-control" placeholder="E-mail:" />
					</div>

					<div class="form-group">
						<textarea class="form-control" name="message" placeholder="Message:" rows="6"></textarea>
					</div>

					<div class="form-group text-right">
						<button class="btn btn-primary" name="send">Send</button>
					</div>

				</form>

			</div>

			<div class="col-sm-offset-1 col-sm-4">

				<div class="info-entry">

					<h4>Address</h4>

					<p>
						Loop, Inc. <br />
						795 Park Ave, Suite 120 <br />
						San Francisco, CA 94107

						<br />
						<br />

						<strong>Working Hours:</strong>
						<br />
						08:00 - 17:00 <br />
						Monday to Friday
						<br />
						<br />
					</p>

				</div>

				<div class="info-entry">

					<h4>Call Us</h4>

					<p>
						Phone: +1 (52) 2215-251<br />
						Fax: +1 (22) 5138-219<br />
						info@laborator.al
					</p>

					<ul class="social-networks">
						<li>
							<a href="#">
								<i class="entypo-instagram"></i>
							</a>
						</li>
						<li>
							<a href="#">
								<i class="entypo-twitter"></i>
							</a>
						</li>
						<li>
							<a href="#">
								<i class="entypo-facebook"></i>
							</a>
						</li>
					</ul>

				</div>

			</div>

		</div>

	</div>

</section>
