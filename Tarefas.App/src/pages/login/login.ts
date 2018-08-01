import { UsuarioService } from './../../providers/usuario.service';
import { UtilService } from './../../providers/util.services';
import { Component } from '@angular/core';
import { NavController, NavParams, IonicPage, MenuController } from 'ionic-angular';
import { FormBuilder, FormGroup, Validators } from '../../../node_modules/@angular/forms';

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {

  public formLogin: FormGroup;
  usuario: string;
  idUsuario: string;

  constructor(public navCtrl: NavController,
    public navParams: NavParams,
    private fb: FormBuilder,
    private utilService: UtilService,
    private usuarioService: UsuarioService,
    public menu: MenuController

  ) {

    this.formLogin = this.fb.group({
      email: ['', Validators.compose([
        Validators.required
      ])],
      senha: ['', Validators.compose([
        Validators.required
      ])]
    })
  }

  ionViewWillEnter() {
    this.menu.swipeEnable(false);
  }

  ionViewDidLeave() {
    this.menu.swipeEnable(true);
  }


  ionViewDidLoad() {

    let token: any = localStorage.getItem('tarefaToken');

    if (token != null) {

      this.navCtrl.setRoot('HomePage');
    }
  }

  cadastrar() {
    this.navCtrl.push('NovoUsuarioPage');
  }

  loginUsuario() {

    let token: any = localStorage.getItem('tarefaToken');

    if (token != null) {

      this.navCtrl.setRoot('HomePage');

    } else {

      this.autenticarUsuario(this.formLogin.value);

    }
  }

  autenticarUsuario(request: any) {

    let loading = this.utilService.showLoading('Autenticando...');
    loading.present();

    this.usuarioService.autenticar(request)
      .then((response) => {
        let autenticado: boolean = response.json().authenticated;

        if (autenticado == false) {
          this.usuario = 'Tarefas';
          loading.dismiss();
          this.utilService.showToast("E-mail ou senha inválidos!");
          return;
        }

        let token: string = response.json().accessToken;
        let primeiroNome: string = response.json().primeiroNome;
        this.idUsuario = response.json().idUsuario;

        //Salvar this.storage.
        localStorage.setItem('tarefaToken', token);
        localStorage.setItem('tarefaUsuario', primeiroNome);
        localStorage.setItem('taerfaUserId', this.idUsuario)


        this.usuario = 'Tarefas - Olá, ' + localStorage.getItem('tarefaUsuario');

        loading.dismiss();
        this.navCtrl.setRoot('HomePage');
        // this.navCtrl.pop();

      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }
}
