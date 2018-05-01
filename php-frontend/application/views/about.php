
<!-- Breadcrumb -->
<section class="breadcrumb">

	<div class="container">

		<div class="row">

			<div class="col-sm-12">

				<h1>Acerca de Nosotros</h1>

							<ol class="breadcrumb bc-3" >
						<li>
				<a href="<?php echo site_url(); ?>"><i class="fa-home"></i>Inicio</a>
			</li>
				<li class="active">

							<strong>Acerca de Nosotros</strong>
					</li>
					</ol>

			</div>

		</div>

	</div>

</section>


<!-- About Us Text -->
<section class="content-section">

	<div class="container">

		<div class="row">

			<div class="col-sm-7">

				<h3>Tu Voto Cuenta </h3>

				<br />

				<p>

					#TuVotoCuenta surge como consecuencia de la inquietud de un colectivo de ciudadanos Mexicanos con el fin de llevar a la práctica sistemas de seguridad electrónica que nos permitan contar con un proceso electoral más transparente, y en consecuencia, seguro y confiable.

					Dicha inquietud surge debido a la gran apatía hacia el proceso electoral, ya que no se tiene confianza en la transparencia del mismo. #TuVotoCuenta está pensado para devolver dicha confianza a la ciudadanía, de modo que se vaya generando una mayor participación en tan importante acto cívico.
				</p>
				<p><br>
					<strong>¿En qué consiste #TuVotoCuenta?</strong><br/>

					El objetivo principal es generar una plataforma pública de consulta basada en el sistema blockchain, que permita almacenar de manera segura una base de datos descentralizada alimentada por la misma ciudadanía mediante una app que permita recabar los datos de salida a colocarse de forma pública en cada casilla; mediante el registro fotográfico de las mantas o carteles en los cuales deberán manifestarse los resultados de cada casilla correspondiente.

					Conforme vaya recabándose la información de las casillas en tiempo real, se realizará un registro criptográfico de los datos y se ingresarán en un bloque de datos (blockchain), dando una mucho mayor garantía a la inmutabilidad e inviolabilidad de los mismos; con lo cual se contará con una referencia confiable de datos que permitirán a cualquier organización o individuo utilizarlos para hacer cualquier tipo de análisis con los mismos.

				</p>
				<p>
					<br>
					<strong>¿Qué seguridad existe para garantizar que la recaudación de información no sea modificada?</strong><br/>

					La app funcionará mediante geolocalización, para confirmar que la persona que haya tomado la foto de la manta o cartel, efectivamente se encuentre en la casilla que corresponda.

					El guardado de registros en una base de datos tipo “Blockchain” garantiza la inmutabilidad de la información; en el caso que cualquier dato fuera alterado, se contará con un indicador verificable que mostrará de manera clara que hubo una modificación al dato registrado originalmente.

					Asimismo, cuando una casilla aparezca en varios registros con votos diferentes se mantendrá fuera o se esperará a que aparezca con al menos tres ID diferentes (del dispositivo) que estén de acuerdo en los votos. Si esto no ocurre, la Foto de la casilla tendrá que ser inspeccionada ocularmente por voluntarios independientes a los notarios y se relegará al conteo lento.

				</p>
				<p>
					<br/>
					<strong>¿Qué datos se recaban?</strong><br/>

					La plataforma de #TuVotoCuenta hace uso de los siguientes datos de acceso público:

					Número o identidad de la casilla de votación

					Sección de la casilla (o estado)

					Marca temporal de la Localización (Latitud y Longitud, via GPS del teléfono del verificador);

					Estado, Municipio, Ciudad, Población (Colonia/Pueblo/Barrio);

					Señas particulares;

					Votantes Totales, votos nulos, votos por candidatos no registrados, votantes ausentes

					Votos por partido/candidato;

					Marca de tiempo de la foto;

					ID del dispositivo móvil “hasheado” (es un cifrado de datos irreversible que nos permite identificar cada dispositivo registrado mediante su registro cifrado; pero no nos permite descifrar el registro para ver el identificador original del teléfono.)

				</p>

			</div>

			<div class="col-sm-5">

				<a href="#">
					<img src="<?php echo base_url('frontend-assets/assets/images/about-img-1.png'); ?>" class="img-rounded" />
				</a>

			</div>

		</div>

	</div>

</section>

<!-- Members -->
<section class="content-section">

	<div class="container">

		<div class="row">

			<?php for ($i=0; $i < 6; $i++) { ?>
				<div class="col-sm-4">

					<div class="staff-member">

						<a class="image" href="#">
							<img src="<?php echo base_url('frontend-assets/assets/images/staff-member.png'); ?>" class="img-circle" />
						</a>

						<h4>
							<a href="#">Monsieur <?php echo $i; ?></a>
							<small>Area <?php echo $i; ?></small>
						</h4>

						<p>Don chinguetas en esto y aquello.</p>

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

			<?php } ?>
		</div>

	</div>

</section>
