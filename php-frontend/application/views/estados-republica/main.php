
	<!-- Breadcrumb -->
<section class="breadcrumb">

	<div class="container">

		<div class="row">

			<div class="col-sm-9">

				<h1>Estados de la República</h1>

							<ol class="breadcrumb bc-3" >
						<li>
				<a href="<?php echo site_url(); ?>"><i class="fa-home"></i>Inicio</a>
			</li>
				<li class="active">

							<strong>Estados de la República Mexicana</strong>
					</li>
					</ol>

			</div>

			<div class="col-sm-3">

				<h2 class="text-muted text-right">{#09} contador</h2>

			</div>

		</div>

	</div>

</section>


<!-- Blog -->
<section class="blog">

	<div class="container">

		<div class="row">

			<div class="col-sm-8">

				<div class="blog-posts">
					<?php
					$directory = FCPATH.'frontend-assets/assets/images/banderas-mexico/';
					$scanned_directory = array_diff(scandir($directory), array('..', '.'));
					// echo "<pre>",print_r($scanned_directory, 1),"</pre>";
					?>

					<?php foreach($scanned_directory as $i => $file) { ?>

					<!-- Blog Post -->
					<div class="blog-post">

						<div class="post-thumb">

							<a href="<?php echo site_url('estados_republica/estado') ?>">
								<img src="<?php echo base_url('frontend-assets/assets/images/banderas-mexico/'.$file) ?>" class="img-rounded" style="max-width:60px; max-height:86px;"/>
								<span class="hover-zoom"></span>
							</a>

						</div>

						<div class="post-details">

							<h3>
								<a href="<?php echo site_url("estados_republica/estado/") ?>"><?php echo str_replace('_', ' ', substr($file, 0, strrpos($file, '.'))); ?></a>
							</h3>

							<div class="post-meta">

								<div class="meta-info">
									<i class="entypo-calendar"></i> Fecha Hoy						</div>

								<div class="meta-info">
									<i class="entypo-comment"></i>
									331 fotos recibidas
								</div>

							</div>

							<p>Paid was hill sir high. For him precaution any advantages dissimilar comparison few terminated projecting. Prevailed discovery immediate objection of ye at. Repair summer one winter living feebly pretty his. In so sense am known these since.</p>

						</div>

					</div>

				<?php } ?>
				<?php /* ?>
					<!-- Blog Pagination -->
					<div class="text-center">

						<ul class="pagination">
							<li class="active">
								<a href="#">1</a>
							</li>
							<li>
								<a href="#">2</a>
							</li>
							<li>
								<a href="#">3</a>
							</li>
							<li>
								<a href="#">4</a>
							</li>
							<li>
								<a href="#">5</a>
							</li>
							<li>
								<a href="#">Next</a>
							</li>
						</ul>

					</div>
				<?php */ ?>
				</div>

			</div>

			<div class="col-sm-4">

<!-- Comments Sidebar -->
<div class="sidebar">

	<h3>
		<i class="entypo-chat"></i>
		Últimas fotos
	</h3>

	<div class="sidebar-content">

		<ul class="discussion-list">
			<?php for ($i=0; $i < 5; $i++) {?>

			<li>
				<a href="#" class="thumb">
					<img src="<?php echo base_url('frontend-assets/assets/images/user-icon-1.png'); ?>" width="43" class="img-circle" />
				</a>

				<div class="details">
					<a href="#">GPS / Location</a>
					<p>Municipio...</p>
					<p>Colonia...</p>
					<p>Sección...</p>
				</div>
			</li>
		<?php } ?>

		</ul>

	</div>

</div>
			</div>

		</div>

	</div>

</section>