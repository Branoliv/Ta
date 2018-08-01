import { UtilService } from './util.services';
import 'rxjs/add/operator/toPromise';
import { Injectable } from "@angular/core";
import { Headers, Http, Response } from '@angular/http';


@Injectable()
export class TarefaService {

    constructor(
        public http: Http,
        public utilService: UtilService,
    ) { }

    listarPorUsuario(): Promise<Response> {
        
        let host = this.utilService.obterHostDaApi();
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));
        
        return this.http.get(host + 'api/v1/Tarefa/Listar/', { headers: headers }).toPromise();
    }

    listarPorLista(idLista: any): Promise<Response> {
        
        let host = this.utilService.obterHostDaApi();
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));
        
        return this.http.get(host + 'api/v1/Tarefa/ListarPorLista/' + idLista, { headers: headers }).toPromise();
    }

    listarPorTarefa(idTarefa: string): Promise<Response> {
        
        let host = this.utilService.obterHostDaApi();

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));
        
        return this.http.get(host + 'api/v1/Tarefa/Obter/' + idTarefa, { headers: headers }).toPromise();
    }

    adicionar(request: any): Promise<Response> {
        
        let host = this.utilService.obterHostDaApi();

        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));

        return this.http.post(host + 'api/v1/Tarefa/Adicionar/', request, { headers: headers }).toPromise();
    }

    atualizar(request: any): Promise<Response> {
        
        let host = this.utilService.obterHostDaApi();

        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));

        return this.http.post(host + 'api/v1/Tarefa/Atualizar/', request, { headers: headers }).toPromise();
    }

    excluir(request:any): Promise<Response>{
        
        let host = this.utilService.obterHostDaApi();

        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization','Bearer ' + localStorage.getItem('tarefaToken'));
        
        return this.http.delete(host + 'api/v1/Tarefa/Excluir/' + request, {headers: headers}).toPromise();

    }
}