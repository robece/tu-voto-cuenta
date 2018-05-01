<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Estados_republica extends CI_Controller {

	/**
	 * Index Page for this controller.
	 *
	 * Maps to the following URL
	 * 		http://domain-name/index.php/blog
	 *	- or -
	 * 		http://domain-name/index.php/blog/index
	 *	- or -
	 * Since this controller is set as the default controller in
	 * config/routes.php, it's displayed at http://domain-name/
	 *
	 * So any other public methods not prefixed with an underscore will
	 * map to /index.php/home/<method_name>
	 * @see https://codeigniter.com/user_guide/general/urls.html
	 */
	public function index()
	{
		$this->load->view('snippets/header');
		$this->load->view('blog');
		$this->load->view('snippets/footer');
	}

	public function estado()
	{
		$this->load->view('snippets/header');
		$this->load->view('blog-post');
		$this->load->view('snippets/footer');
	}
}
