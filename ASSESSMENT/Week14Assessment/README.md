# Week14 Assessment Deliverables

This folder contains the recovered Week 14 assessment outputs.

## Final File

- `Week14 Assessment One.pdf` -> Final submission-ready document.

## Source Inputs

- `Week14 Assessment - Azure VPN.docx` -> Original instruction content used to rebuild the file.
- `images/` -> Screenshot images placed in step order.

## Image Placement Order Used

1. Step 2 -> `vnet1.jpeg`
2. Step 3 -> `connection of vnet2.jpeg`
3. Step 4 -> `resource for vpn.jpeg`
4. Step 6 -> `connection vnetGateway.jpeg`
5. Step 7 -> `connection device of vnet1.jpeg`
6. Step 9 -> `step 9 ping command for vm1.jpeg`
7. Step 9 -> `ping from vm2 to vm1 on private network.jpeg`
8. Final overview -> `overall.jpeg`

## Regeneration Script

- `build_week14_pdf.py` generates the PDF from the original Word instructions and image folder.

Run command:

```powershell
& "c:/sahil/study material/SEM-8/CAPGEMINI/CU-DotNet-Jan26-B4/CU-DotNet-Jan26-B4-Sahil-Ittan/ASSESSMENT/Week14Assessment/.venv/Scripts/python.exe" "c:/sahil/study material/SEM-8/CAPGEMINI/CU-DotNet-Jan26-B4/CU-DotNet-Jan26-B4-Sahil-Ittan/ASSESSMENT/Week14Assessment/build_week14_pdf.py"
```
