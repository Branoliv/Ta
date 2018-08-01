import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UtilService } from '../../providers/util.services';
import { ListaDeTarefasService } from '../../providers/listadetarefas.service';
import { TarefaService } from '../../providers/tarefa.service';


@IonicPage()
@Component({
  selector: 'page-editar-tarefa',
  templateUrl: 'editar-tarefa.html',
})
export class EditarTarefaPage {

  public form: FormGroup;
  idTarefa: any;
  tarefas: any[] = [];
  listas: any[] = [];

  constructor(public navCtrl: NavController,
    public navParams: NavParams,
    private fb: FormBuilder,
    private utilService: UtilService,
    private tarefaService: TarefaService,
    private listaDeTarefasService: ListaDeTarefasService,
    private alertCtrl: AlertController) {

    this.idTarefa = this.navParams.get('idTarefa');

    this.form = this.fb.group({
      idtarefa: [this.idTarefa, Validators.compose([
        Validators.required
      ])],
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

      ])],
      status: ['', Validators.compose([

      ])]
    })


  }

  ionViewDidEnter() {

    if (this.idTarefa == null) {
      
      this.navCtrl.pop();
      
    } else {
      this.loadTarefa(this.idTarefa);
    }
  }

  loadTarefa(idTarefa: any) {

    let loading = this.utilService.showLoading();
    loading.present();

    //Carrega lista de tarefas => select, options
    this.listaDeTarefasService.listar()
      .then((response) => {
        this.listas = response.json();

      }).catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });

    //Retornar tarefa selecionada
    this.tarefaService.listarPorTarefa(idTarefa)
      .then((response) => {
        this.tarefas = response.json();

        loading.dismiss();
      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }

  loadListaDeTarefas() {
    //Desabilitado
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

    this.tarefaService.atualizar(this.form.value)
      .then(() => {
        loading.dismiss();
        this.utilService.showAlert("Salvo com sucesso");
        this.form.reset();
      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      })
    this.navCtrl.pop();
  }

  cancelar() {
    this.navCtrl.pop();
  }

  excluir() {
    let alert = this.alertCtrl.create({
      title: 'Atenção',
      message: 'Deseja excluir a tarefa?',
      buttons: [
        {
          text: 'Não',
          role: 'cancel',
          handler: () => {

          }
        },
        {
          text: 'Sim',
          handler: () => {

            this.tarefas = this.form.value;
            this.excluirTarefa(this.tarefas);
            this.form.reset();
          }
        }
      ]
    });
    alert.present();
  }

  excluirTarefa(Tarefa: any) {

    let loading = this.utilService.showLoading();
    loading.present();

    this.tarefaService.excluir(Tarefa.idtarefa)
      .then((response) => {
        this.utilService.showAlert(response.json().message);
        loading.dismiss();
      })
      .catch((response) => {
        loading.dismiss();
        this.utilService.showMessageError(response);
      });
  }
}