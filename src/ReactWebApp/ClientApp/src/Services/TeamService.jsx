import { HandleResponseError, HandleResponseErrorGet } from "./ResponseErrorHandling";

export const LoadTeams = (setTeams, seasonId) => {
    return(
    fetch(`Api/Team/Season/${seasonId}`)
        .then(HandleResponseErrorGet)
        .then((data) => setTeams(data))
        .catch((error) => {
            alert(error);
            console.error('Error:', error);
        })
    )
}

export const LoadAllTeams = (setTeams) => {
    return(
    fetch(`Api/Team`)
        .then(HandleResponseErrorGet)
        .then((data) => setTeams(data))
        .catch((error) => {
            alert(error);
            console.error('Error:', error);
        })
    )
}

export const EditTeam = (inputs) => {
    return (
        fetch('api/Team', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}

export const AddTeam = (inputs) => {
    return (
        fetch('api/Team', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(inputs)
        })
            .then(HandleResponseError)
            .catch(error => {
                alert(error);
                console.error('Error:', error);
            })
    );
}