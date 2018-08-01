import { UsuarioService } from './../../providers/usuario.service';
import { UtilService } from './../../providers/util.services';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { FormGroup, Validators, FormBuilder } from '../../../node_modules/@angular/forms';


@IonicPage()
@Component({
  selector: 'page-editar-usuario',
  templateUrl: 'editar-usuario.html',
})
export class EditarUsuarioPage {

  public form: FormGroup;
  usuario: any[] = [];
  idUsuario: string;

  constructor(public navCtrl: NavController,
    private fb: FormBuilder,
    public navParams: NavParams,
    private utilService: UtilService,
    private usuarioService: UsuarioService) {

      
      this.usuario = this.navParams.get('usuario');


    this.form = this.fb.group(
      {
        primeiroNome: ['', Validators.compose([
          Validators.minLength(1),
          Validators.maxLength(50),
          Validators.required,
        ])],

        ultimoNome: ['', Validators.compose([
          Validators.minLength(1),
          Validators.maxLength(50),
          Validators.required
        ])],

        email: ['', Validators.compose([
          Validators.minLength(5),
          Validators.maxLength(200),
          Validators.required
        ])],

        logradouro: ['', Validators.compose([])],

        numeroResid: ['', Validators.compose([])],

        bairro: ['', Validators.compose([])],

        cidade: ['', Validators.compose([])],

        estado: ['', Validators.compose([])],

        pais: ['', Validators.compose([])],

        cep: ['', Validators.compose([])]

      }
    );
  }

  ionViewDidLoad() {
    this.idUsuario = localStorage.getItem('taerfaUserId');
  }

  salvar() {
    let loading = this.utilService.showLoading();
    loading.present();

    this.usuarioService.atualizarUsuario(this.form.value)
      .then((response) => {
        loading.dismiss();
        this.utilService.showAlert('Salvo com sucesso!')
        localStorage.setItem('usuario.email', this.form.value.email);
        this.navCtrl.pop();
      })

      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }

  cancelar() {
    this.navCtrl.pop();
  }
}
