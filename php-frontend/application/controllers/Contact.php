<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Contact extends CI_Controller {

	/**
	 * Index Page for this controller.
	 *
	 * Maps to the following URL
	 * 		http://domain-name/index.php/contact
	 *	- or -
	 * 		http://domain-name/index.php/contact/index
	 *	- or -
	 * Since this controller is set as the default controller in
	 * config/routes.php, it's displayed at http://domain-name/
	 *
	 * So any other public contact prefixed with an underscore will
	 * map to /index.php/welcome/<method_name>
	 * @see https://codeigniter.com/user_guide/general/urls.html
	 */
	public function index()
	{
		$this->load->view('snippets/header');
		$this->load->view('contact');
		$this->load->view('snippets/footer');
	}
}
