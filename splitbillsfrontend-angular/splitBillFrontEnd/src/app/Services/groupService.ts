import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from './tokenService';
import GroupRequestDTO from '../models/GroupDTO';
import { CreateGroupDTO } from '../models/CreateGroupDTO';


import { Observable } from 'rxjs';
import  GroupDetailsDTO  from '../models/GroupDetailsDTO';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  URL = 'https://localhost:44373/api/Group';

  constructor(private _httpClient: HttpClient) {}

  public res: GroupRequestDTO[] = [];

  createGroup(group: CreateGroupDTO) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });
    return this._httpClient.post(`${this.URL}/CreateGroup`, group, {
      headers: head,
      responseType: 'text',
    });
  }

  fetchAllGroup() {
    const head = new HttpHeaders({
      'Content-Type': 'application/type',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });
    return this._httpClient.get<GroupRequestDTO[]>(
      `${this.URL}/GetAnGroupsById`,
      {
        headers: head,
      }
    );
  }

  deleteGroup(id: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.delete(
      `${this.URL}/DeleteGroupById?groupId=${id}`,
      {
        headers: head,
      }
    );
  }

  leaveGroup(id: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.delete(`${this.URL}/LeaveGroup?groupId=${id}`, {
      headers: head,
    });
  }

  getIndividualGroupDetails(groupId: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/type',

      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.get<GroupDetailsDTO[]>(
      `${this.URL}/GetDetailsGroupById?id=${groupId}`,

      {
        headers: head,
      }
    );
  }

  getIndivisualGroup(groupId: string): Observable<GroupDetailsDTO> {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.get<GroupDetailsDTO>(
      `${this.URL}/GetDetailsGroupById?id=${groupId}`,
      { headers: head }
    );
  }

  addUser(groupId: string, userEmail: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.put(
      `${this.URL}/AddUserIntoExistingGroup?groupId=${groupId}`,
      JSON.stringify(userEmail),
      { headers: head, responseType: 'text' }
    );
  }

  deleteUserFromGroup(groupId: string, userId: string) {
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('jwt')}`,
    });

    return this._httpClient.delete(
      `${this.URL}/RemoveUser?groupId=${groupId}&userEmailId=${userId}`,
      { headers: head }
    );
  }
  getOne(groupId:string):Observable<GroupDetailsDTO>{

    // console.log('view details of a group service called');

    const head = new HttpHeaders({

      'Content-Type': 'application/json',

      Authorization: `Bearer ${localStorage.getItem('jwt')}`,

    });

    return this._httpClient.get<GroupDetailsDTO>(`${this.URL}/GetDetailsGroupById?id=${groupId}`, {headers:head});

  }
}
