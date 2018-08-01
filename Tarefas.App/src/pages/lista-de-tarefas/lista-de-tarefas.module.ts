import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ListaDeTarefasPage } from './lista-de-tarefas';

@NgModule({
  declarations: [
    ListaDeTarefasPage,
  ],
  imports: [
    IonicPageModule.forChild(ListaDeTarefasPage),
  ],
})
export class ListaDeTarefasPageModule {}
