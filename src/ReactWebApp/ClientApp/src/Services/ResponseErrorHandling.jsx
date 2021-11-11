export const HandleResponseErrorGet = ( data ) => 
{
    console.log(data);
    if (data.responseStatus && data.responseStatus !== 404) {
        alert(data.validationErrors);
    }
    if(data.status === 500)
    {
        alert(data.detail);
    }
}

export const HandleResponseError = ( data ) => 
{
    console.log(data);
    if (data.responseStatus) {
        alert(data.validationErrors);
    }
    if(data.status === 500)
    {
        alert(data.detail);
    }
}