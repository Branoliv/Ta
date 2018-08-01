import { Component } from '@angular/core';
import { NavController, IonicPage } from 'ionic-angular';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UtilService } from '../../providers/util.services';
import { UsuarioService } from '../../providers/usuario.service';


@IonicPage()
@Component({
  selector: 'page-novo-usuario',
  templateUrl: 'novo-usuario.html',
})
export class NovoUsuarioPage {

  public form: FormGroup;

  constructor(private fb: FormBuilder,
    private utilService: UtilService,
    private usuarioService: UsuarioService,
    private navCtrl: NavController,
  ) {

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

        senha: ['', Validators.compose([
          Validators.minLength(5),
          Validators.maxLength(36),
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

  }

  salvar() {
    let loading = this.utilService.showLoading();
    loading.present();

    this.usuarioService.adicionar(this.form.value)

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
