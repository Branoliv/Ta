import { ListaDeTarefasService } from './../../providers/listadetarefas.service';
import { UtilService } from './../../providers/util.services';
import { Component } from '@angular/core';
import { AlertController, ViewController, NavController, IonicPage } from 'ionic-angular';

@IonicPage()
@Component({
  selector: 'page-adicionar-lista-de-tarefas',
  templateUrl: 'adicionar-lista-de-tarefas.html',
})
export class AdicionarListaDeTarefasPage {

  listas: any[] = [];

  constructor(private alertCtrl: AlertController,
    private utilService: UtilService,
    private listaDeTarefasService: ListaDeTarefasService,
    private viewCtrl: ViewController,
    private navCtrl: NavController,
  ) { }

  ionViewDidLoad() {
    this.loadListaDeTarefas();
  }

  adicionar() {

    let prompt = this.alertCtrl.create({

      title: 'Adicionar lista de tarefas',
      message: "Informe os dados da lista",
      inputs: [
        {
          name: 'nome',
          placeholder: 'Nome da lista',
          type: 'text'
        }
      ],
      buttons: [
        {
          text: 'Cancelar',
          handler: data => {

          }
        },
        {
          text: 'Salvar',
          handler: data => {
            let loading = this.utilService.showLoading();
            loading.present();

            this.listaDeTarefasService.adicionar(data.nome)
              .then((response) => {
                loading.dismiss();
                this.loadListaDeTarefas();

              })
              .catch((response) => {
                loading.dismiss();
                this.utilService.showMessageError(response);
              });
          }
        }
      ]
    });
    prompt.present();
  }

  excluir(listaDeTarefas: any) {

    let loading = this.utilService.showLoading();
    loading.present();

    this.listaDeTarefasService.excluir(listaDeTarefas.id)
      .then((response) => {
        this.utilService.showAlert(response.json().message);
        this.loadListaDeTarefas();
        loading.dismiss();
      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }

  closeModal() {
    this.viewCtrl.dismiss();
  }

  loadListaDeTarefas() {

    let loading = this.utilService.showLoading();
    loading.present();

    this.listaDeTarefasService.listar()
      .then((response) => {
        this.listas = response.json();
        loading.dismiss();
      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }

  showListaDeTarefas(lista: any) {


    let token: string = localStorage.getItem('tarefaToken')

    if (token != null) {
      this.navCtrl.push('ListaDeTarefasPage', { id: lista.id, nome: lista.nome });
    }
    else {
      alert('Login expirado!');
    }
  }
}
