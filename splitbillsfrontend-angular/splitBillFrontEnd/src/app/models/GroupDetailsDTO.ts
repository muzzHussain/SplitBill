export default interface GroupDetailDTO {
  id: string;

  groupName: string;

  groupMembers: Users[];
}

export interface Users {
  emailId: string;
  name: string;
  UserId: string;
}
