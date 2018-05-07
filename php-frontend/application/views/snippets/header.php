<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="X-UA-Compatible" content="IE=edge">

	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<meta name="description" content="Tu Voto Cuenta" />
	<meta name="author" content="{Colectivo-Todos}" />

	<link rel="icon" href="<?php echo base_url('frontend-assets/assets/images/favicon.ico'); ?>">

	<title>Tu Voto Cuenta 2018</title>

	<link rel="stylesheet" href="<?php echo base_url('frontend-assets/assets/css/bootstrap.css'); ?>">
	<link rel="stylesheet" href="<?php echo base_url('frontend-assets/assets/css/font-icons/entypo/css/entypo.css'); ?>">
	<link rel="stylesheet" href="<?php echo base_url('frontend-assets/assets/css/neon.css'); ?>">

	<script src="<?php echo base_url('frontend-assets/assets/js/jquery-1.11.3.min.js'); ?>"></script>

	<!--[if lt IE 9]><script src="<?php echo base_url('frontend-assets/assets/js/ie8-responsive-file-warning.js') ?>"></script><![endif]-->

	<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
	<![endif]-->


</head>
<body>

<div class="wrap">

	<!-- Logo and Navigation -->
<div class="site-header-container container">

	<div class="row">

		<div class="col-md-12">

			<header class="site-header">

				<section class="site-logo">

					<a href="/">
						<img src="<?php echo base_url('frontend-assets/assets/images/Artboard 8.png'); ?>" width="120" />
					</a>

				</section>

				<nav class="site-nav">

					<ul class="main-menu hidden-xs" id="main-menu">
						<li <?php if ($this->router->class == 'home') { echo 'class="active"'; } ?>>
							<a href="<?php echo site_url(); ?>">
								<span>Inicio</span>
							</a>
						</li>
            <li <?php if ($this->router->class == 'about') { echo 'class="active"'; } ?>>
              <a href="<?php echo site_url('about'); ?>">
                <span>Nosotros</span>
              </a>
            </li>

						<li <?php if ($this->router->class == 'estados_republica') { echo 'class="active"'; } ?>>
							<a href="<?php echo site_url('estados_republica') ?>">
								<span>Estados de la República</span>
							</a>
						</li>
						<li <?php if ($this->router->class == 'contacto') { echo 'class="active"'; } ?>>
							<a href="<?php echo site_url('contact') ?>">
								<span>Contacto</span>
							</a>
						</li>
						<li class="search">
							<a href="#">
								<i class="entypo-search"></i>
							</a>

							<form method="get" class="search-form" action="" enctype="application/x-www-form-urlencoded">
								<input type="text" class="form-control" name="q" placeholder="Busca tu municipio..." />
							</form>
						</li>
					</ul>


					<div class="visible-xs">

						<a href="#" class="menu-trigger">
							<i class="entypo-menu"></i>
						</a>

					</div>
				</nav>

			</header>

		</div>

	</div>

</div>
