import { ListaDeTarefasService } from './../../providers/listadetarefas.service';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { UtilService } from '../../providers/util.services';
import { TarefaService } from '../../providers/tarefa.service';


@IonicPage()
@Component({
  selector: 'page-lista-de-tarefas',
  templateUrl: 'lista-de-tarefas.html',
})
export class ListaDeTarefasPage {

  id: any;
  nomeLista:any;
  listas: any[] = [];
  tarefas: any[] = [];

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private utilService: UtilService,
    private tarefaService: TarefaService,
    private listaDeTarefasService: ListaDeTarefasService) {
    
      
      this.id = this.navParams.get('id');
      this.nomeLista = this.navParams.get('nome');
  }
  
  ionViewDidLoad() {
    this.loadTarefa(this.id);
  }

  loadTarefa(id: any) {
     
    
    let loading = this.utilService.showLoading();
    loading.present();
    
    this.tarefaService.listarPorLista(id)
      .then((response) => {
        this.listas = response.json();
        loading.dismiss();
      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });

  }

  opeLista(){

  }

  loadListaDeTarefa() {
    
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
}
