export interface CreateGroupDTO {
  title: string;
  UsersList: Users[];
}

export interface Users {
  EmailId: string;
}
