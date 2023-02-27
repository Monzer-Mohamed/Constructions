import {  useState } from 'react';
import { apiPost } from '../../service/Api'
import {APIConst} from './../../service/apiEndPoints';
import {useNavigate} from 'react-router-dom';
// material-ui
import {
    Button,
    FormHelperText,
    Grid,
    InputLabel,
    OutlinedInput,
    Stack,
    Box
} from '@mui/material';

// third party
import * as Yup from 'yup';
import { Formik } from 'formik';

// project import
import AnimateButton from 'components/@extended/AnimateButton';
 
// assets 

// ============================|| FIREBASE - REGISTER ||============================ //

const PaymentForm = () => {
     const [level,setLevel] = useState();
     const navigate  =  useNavigate();
     const ConstructionId  = localStorage.getItem('ConstructionId');
     const ConstructionAmount = localStorage.getItem('Amount');

    return (
        <>
            <Formik
                initialValues={{
                    CardNumber:'',
                    CardExpiry: '',
                    Cvv:'',
                    submit: null
                }}
                validationSchema={Yup.object().shape({
                    CardNumber: Yup.string().max(255).required('Card Number is required'),
                    CardExpiry: Yup.string().max(255).required('Expiry Date is required'),
                    Cvv: Yup.string().required('Cvv is required'),
                    
                })}
                onSubmit={async (values, { setErrors, setStatus, setSubmitting }) => {
                    try {
                       
                      
                       const requestBody ={
                        ...values, 
                        Amount:parseInt(ConstructionAmount)
                        
                       }
                        apiPost(APIConst.Payments,requestBody)
                        .then(res=>{
                            setStatus({ success: false });
                            navigate("/dashboard/default");
                        })
                        setSubmitting(false);
                    } catch (err) {
                        console.error(err);
                        setStatus({ success: false });
                        setErrors({ submit: err.message });
                        setSubmitting(false);
                    }finally{
                        localStorage.clear();
                    }

                }}
            >
                {({ errors, handleBlur, handleChange, handleSubmit, isSubmitting, touched, values }) => (
                   <Box>
                   <form noValidate onSubmit={handleSubmit}>
                        <Grid container spacing={3}>
                            <Grid item xs={5}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="firstname-signup">Card Number *</InputLabel>
                                    <OutlinedInput
                                        id="CardNumber-login"
                                        value={values.CardNumber}
                                        name="CardNumber"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="Card Number"
                                        fullWidth
                                        error={Boolean(touched.CardNumber && errors.CardNumber)}
                                    />
                                    {touched.CardNumber && errors.CardNumber && (
                                        <FormHelperText error id="helper-text-firstname-signup">
                                            {errors.CardNumber}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid> 
                            <Grid item xs={2}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="lastname-signup">Expiry Date *</InputLabel>
                                    <OutlinedInput
                                        fullWidth
                                        error={Boolean(touched.CardExpiry && errors.CardExpiry)}
                                        id="CardExpiry"
                                        type="datetime"
                                        value={values.CardExpiry}
                                        name="CardExpiry"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="Expiry Date"
                                        inputProps={{}}
                                    />
                                    {touched.CardExpiry && errors.CardExpiry && (
                                        <FormHelperText error id="helper-text-lastname-signup">
                                            {errors.CardExpiry}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid> 
                            <Grid item xs={12}>
                                </Grid>
                                
                            <Grid item xs={2}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="company-signup">Cvv</InputLabel>
                                    <OutlinedInput
                                        fullWidth 
                                        error={Boolean(touched.Cvv && errors.Cvv)}
                                        id="company-signup"
                                        value={values.Cvv}
                                        name="Cvv"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="Cvv"
                                        inputProps={{}}
                                    />
                                    {touched.Cvv && errors.Cvv && (
                                        <FormHelperText error id="helper-text-company-signup">
                                            {errors.Cvv}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid>
                            {errors.submit && (
                                <Grid item xs={6}>
                                    <FormHelperText error>{errors.submit}</FormHelperText>
                                </Grid>
                            )}
                              <Grid item xs={12}>
                                </Grid>
                                <Grid item xs={2}>
                                <Stack spacing={1}>
                                    <InputLabel htmlFor="amount-signup">Amount</InputLabel>
                                    <OutlinedInput
                                        fullWidth
                                        error={Boolean(touched.amount && errors.amount)}
                                        id="amount"
                                        type="amount"
                                        value={ConstructionAmount}
                                        name="price"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        placeholder="Amount"
                                        disabled
                                        inputProps={{}}
                                    />
                                    {touched.amount && errors.amount && (
                                        <FormHelperText error id="helper-text-lastname-signup">
                                            {errors.amount}
                                        </FormHelperText>
                                    )}
                                </Stack>
                            </Grid> 
                            <Grid item xs={12}>
                                </Grid>
                            <Grid item xs={1}>
                                <AnimateButton>
                                    <Button
                                        disableElevation
                                        disabled={isSubmitting}
                                        fullWidth
                                        size="large"
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                    >
                                        Pay
                                    </Button>
                                </AnimateButton>
                            </Grid>
                        </Grid>
                    </form>
                    </Box>
                )}
            </Formik>
        </>
    );
};

export default PaymentForm;
