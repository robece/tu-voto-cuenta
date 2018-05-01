<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class About extends CI_Controller {

	/**
	 * Index Page for this controller.
	 *
	 * Maps to the following URL
	 * 		http://domain-name/index.php/about
	 *	- or -
	 * 		http://domain-name/index.php/about/index
	 *	- or -
	 * Since this controller is set as the default controller in
	 * config/routes.php, it's displayed at http://domain-name/
	 *
	 * So any other public methods not prefixed with an underscore will
	 * map to /index.php/about/<method_name>
	 * @see https://codeigniter.com/user_guide/general/urls.html
	 */
	public function index()
	{
		$this->load->view('snippets/header');
		$this->load->view('about');
		$this->load->view('snippets/footer');
	}
}
