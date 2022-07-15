const initialState = {
  users: [],
}

export const ActionTypes = {
  SET_USERS: 'SET_USERS',
}

export const ActionCreators = {
  setUsers: payload => ({type: ActionTypes.SET_USERS, payload}),
}

export default function UsersReducer(state = initialState, action) {
  switch (action.type) {
    case ActionTypes.SET_USERS:
      return {...state, users: [action.payload]};
    default:
      return state;
  }
}