import { UtilService } from './util.services';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { Injectable } from "@angular/core";



@Injectable()
export class ListaDeTarefasService {

    constructor(
        public http: Http,
        public utilService: UtilService,
    ) { }

    listar(): Promise<Response> {

        let host = this.utilService.obterHostDaApi();

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));

        return this.http.get(host + 'api/v1/ListaDeTarefas/Listar', {headers: headers}).toPromise();
    }

    adicionar(nome: string): Promise<Response> {

        let host = this.utilService.obterHostDaApi();

        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));

        return this.http.post(host + 'api/v1/ListaDeTarefas/Adicionar/', { nome: nome }, { headers: headers }).toPromise();
    }

    excluir(id: any): Promise<Response> {
        let host = this.utilService.obterHostDaApi();

        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));
        
        return this.http.delete(host + 'api/v1/ListaDeTarefas/Excluir/' + id, { headers: headers }).toPromise();
    }
}