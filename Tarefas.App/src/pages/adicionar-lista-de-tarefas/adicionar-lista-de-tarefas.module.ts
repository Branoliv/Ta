import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { AdicionarListaDeTarefasPage } from './adicionar-lista-de-tarefas';

@NgModule({
    declarations: [
        AdicionarListaDeTarefasPage,
    ],
    imports: [
        IonicPageModule.forChild(AdicionarListaDeTarefasPage),
    ],
})
export class AdicionarListaDeTarefasPageModule { }