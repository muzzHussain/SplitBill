export default interface ExpenseDTO {
  groupId: string;

  title: string;

  usersList: { [userEmail: string]: number };
}
