import { UtilService } from './../../providers/util.services';
import { Component } from '@angular/core';
import { NavController, IonicPage, ToastController, Refresher } from 'ionic-angular';
import { TarefaService } from '../../providers/tarefa.service';

@IonicPage()
@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  tarefas: any[] = [];
  idUsuario: string;
  usuario: string;
  idtarefa: any;
  status: String;

  constructor(public navCtrl: NavController,
    private utilService: UtilService,
    private tarefaService: TarefaService,
    private toastCtrl:ToastController
  ) { }


  ionViewWillEnter() {
    let user: string = localStorage.getItem('tarefaUsuario')

    if (user == null) {
      this.usuario = 'Tarefas';
    }
    else {

      this.usuario = 'Tarefas - Olá, ' + user;
      // this.loadTarefa();
    }
  }

  ionViewDidLoad() {
    this.loadTarefa();
  }

  fecharApp() {

    this.utilService.efetuarLogff();
    this.tarefas = null;
    this.usuario = null;
    this.usuario = 'Tarefas';
    this.ionViewWillEnter();
    this.navCtrl.setRoot('LoginPage');
  }

  loadTarefa() {

    let loading = this.utilService.showLoading('Processando...');
    loading.present();
    let token: any = localStorage.getItem('tarefaToken');

    if (token == null) {
      this.navCtrl.setRoot('LoginPage');
    } else {

      this.tarefaService.listarPorUsuario()
        .then((response) => {

          this.tarefas = response.json();
          loading.dismiss();
        })
        .catch((response) => {
          loading.dismiss();
          this.utilService.showMessageError(response);
        });
    }
  }

  doRefresh(refresher:Refresher) {

    let loading = this.utilService.showLoading('Processando...');
    loading.present();
    let token: any = localStorage.getItem('tarefaToken');

    if (token == null) {
      this.navCtrl.setRoot('LoginPage');
    } else {

      this.tarefaService.listarPorUsuario()
        .then((response) => {

          this.tarefas = response.json();
          loading.dismiss();
        })
        .catch((response) => {
          loading.dismiss();
          this.utilService.showMessageError(response);
        });


        setTimeout(() => {
          refresher.complete();
  
          const toast = this.toastCtrl.create({
            message: 'Lista atualizada.',
            duration: 3000
          });
          toast.present();
        }, 1000);
    }
  }

  showNovaTarefa(id: string) {

    let token: any = localStorage.getItem('tarefaToken');

    if (token != null) {
      this.navCtrl.push('TarefaPage');
    }
    else {
      this.navCtrl.setRoot('LoginPage');
    }
  }

  showEtidarTarefa(tarefa: any) {

    let token: any = localStorage.getItem('tarefaToken');

    if (token != null) {
      this.navCtrl.push('EditarTarefaPage', { idTarefa: tarefa.idTarefa });
    }
    else {
      this.navCtrl.setRoot('LoginPage');
    }
  }
}
