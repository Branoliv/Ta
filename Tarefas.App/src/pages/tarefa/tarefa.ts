import { TarefaService } from './../../providers/tarefa.service';
import { ListaDeTarefasService } from './../../providers/listadetarefas.service';
import { UtilService } from './../../providers/util.services';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ModalController } from 'ionic-angular';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@IonicPage()
@Component({
  selector: 'page-tarefa',
  templateUrl: 'tarefa.html',
})
export class TarefaPage {
  
  public form: FormGroup;
  listas: any[] = [];

  constructor(public navCtrl: NavController,
    public navParams: NavParams,
    private fb: FormBuilder,
    private utilService: UtilService,
    private modalCtrl: ModalController,
    private listaDeTarefasService: ListaDeTarefasService,
    private tarefaService: TarefaService
  ) {

    this.form = this.fb.group({
      titulo: ['', Validators.compose([
        Validators.minLength(1),
        Validators.maxLength(100),
        Validators.required
      ])],

      descricao: ['', Validators.compose([
        Validators.minLength(1),
        Validators.maxLength(500),
        Validators.required
      ])],

      dataInicio: ['', Validators.compose([
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.required
      ])],

      dataConclusao: ['', Validators.compose([
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.required
      ])],

      idListaDeTarefas: ['', Validators.compose([

      ])]
    })

  }

  ionViewDidLoad() {
    this.loadListaDeTarefas();
  }

  loadListaDeTarefas() {

    let loading = this.utilService.showLoading();
    loading.present();

    this.listaDeTarefasService.listar()
      .then((response) => {
        this.listas = response.json();
        loading.dismiss();

      }).catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }

  salvar() {
    

    let loading = this.utilService.showLoading();
    loading.present();

    this.tarefaService.adicionar(this.form.value)
    .then(() => {
      loading.dismiss();
      this.utilService.showAlert("Salvo com sucesso");
      this.navCtrl.pop();
    })
    .catch((response) =>{
      loading.dismiss();
      this.utilService.showMessageError(response);
    })
  }

  cancelar() {
    this.navCtrl.pop();
  }

  showAddListaDeTarefas() {
    let modal = this.modalCtrl.create('AdicionarListaDeTarefasPage');
    
    modal.onDidDismiss(data => {
      this.loadListaDeTarefas();
    });
    modal.present();
  }
}
