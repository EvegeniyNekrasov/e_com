
export interface FieldMetaRegister {
  name: 'firstName' | 'lastName' | 'email' | 'password';
  label: string;
  type: string;
  placeholder: string;
}

export interface FieldMetaLogin {
  name: 'email' | 'password';
  label: string;
  type: string;
  placeholder: string;
}
