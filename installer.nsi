; Script generated by the HM NIS Edit Script Wizard.
!include WordFunc.nsh
!insertmacro VersionCompare
!include LogicLib.nsh

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "EVEPOSMon"
!define PRODUCT_VERSION "Beta2"
!define PRODUCT_PUBLISHER "EXA Nation"
!define PRODUCT_WEB_SITE "http://code.google.com/p/eveposmon"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\EVEPOSMon.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "gpl.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "EVEPOSMon"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\EVEPOSMon.exe"
!define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\readme.txt"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup.exe"
InstallDir "$PROGRAMFILES\EVEPOSMon"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Var InstallDotNET

Function .onInit
  !insertmacro MUI_LANGDLL_DISPLAY

  BringToFront
; Check if already running
; If so don't open another but bring to front
  System::Call "kernel32::CreateMutexA(i 0, i 0, t '$(^Name)') i .r0 ?e"
  Pop $0
  StrCmp $0 0 launch
   StrLen $0 "$(^Name)"
   IntOp $0 $0 + 1
  loop:
    FindWindow $1 '#32770' '' 0 $1
    IntCmp $1 0 +4
    System::Call "user32::GetWindowText(i r1, t .r2, i r0) i."
    StrCmp $2 "$(^Name)" 0 loop
    System::Call "user32::ShowWindow(i r1,i 9) i."         ; If minimized then maximize
    System::Call "user32::SetForegroundWindow(i r1) i."    ; Bring to front
    Abort
  launch:

  ; Check .NET version
  StrCpy $InstallDotNET "No"
  Call GetDotNETVersion
  Pop $0

  ${If} $0 == "not found"
        StrCpy $InstallDotNET "Yes"
  	MessageBox MB_OK|MB_ICONINFORMATION "${PRODUCT_NAME} requires that the .NET Framework 2.0 is installed. The .NET Framework will be downloaded and installed automatically during installation of ${PRODUCT_NAME}."
   	Return
  ${EndIf}

  StrCpy $0 $0 "" 1 # skip "v"

  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2
        StrCpy $InstallDotNET "Yes"
  	MessageBox MB_OK|MB_ICONINFORMATION "${PRODUCT_NAME} requires that the .NET Framework 2.0 is installed. The .NET Framework will be downloaded and installed automatically during installation of ${PRODUCT_NAME}."
   	Return
  ${EndIf}
FunctionEnd

Section "MainSection" SEC01
  SetOutPath "$INSTDIR\data"
  SetOverwrite try
  
    ; Get .NET if required
  ${If} $InstallDotNET == "Yes"
     SetDetailsView hide
     ;requires the inetc plugin!!!!
     inetc::get /caption "Downloading .NET Framework 2.0" /canceltext "Cancel" "http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe" "$INSTDIR\dotnetfx.exe" /end
     Pop $1

     ${If} $1 != "OK"
           Delete "$INSTDIR\dotnetfx.exe"
           Abort "Installation cancelled."
     ${EndIf}

     ExecWait "$INSTDIR\dotnetfx.exe"
     Delete "$INSTDIR\dotnetfx.exe"

     SetDetailsView show
  ${EndIf}
  
  File "bin\Release\data\controlTowers.xml.gz"
  File "bin\Release\data\invControlTowerResources.xml.gz"
  File "bin\Release\data\mapData.xml.gz"
  File "bin\Release\data\moonData.xml.gz"
  SetOutPath "$INSTDIR"
  File "bin\Release\EVEPOSMon.exe"
  File "bin\Release\EVEPOSMon.pdb"
  File "bin\Release\EVEPOSMon.vshost.exe"
  SetOutPath "$INSTDIR\icons"
  File "bin\Release\icons\icon06_02.png"
  File "bin\Release\icons\icon06_03.png"
  File "bin\Release\icons\icon06_06.png"
  File "bin\Release\icons\icon06_07.png"
  File "bin\Release\icons\icon07_04.png"
  File "bin\Release\icons\icon07_09.png"
  File "bin\Release\icons\icon07_12.png"
  File "bin\Release\icons\icon09_02.png"
  File "bin\Release\icons\icon09_04.png"
  File "bin\Release\icons\icon09_08.png"
  File "bin\Release\icons\icon10_07.png"
  File "bin\Release\icons\icon10_10.png"
  File "bin\Release\icons\icon12_01.png"
  File "bin\Release\icons\icon12_07.png"
  File "bin\Release\icons\icon24_06.png"
  File "bin\Release\icons\icon24_14.png"
  File "bin\Release\icons\icon25_08.png"
  File "bin\Release\icons\icon27_10.png"
  File "bin\Release\icons\icon33_02.png"
  File "bin\Release\icons\icon34_06.png"
  File "bin\Release\icons\icon40_14.png"
  File "bin\Release\icons\icon47_01.png"
  File "bin\Release\icons\icon47_02.png"
  File "bin\Release\icons\icon47_03.png"
  File "bin\Release\icons\icon47_04.png"
  File "bin\Release\icons\icon47_05.png"
  File "bin\Release\icons\icon47_06.png"
  File "bin\Release\icons\icon47_07.png"
  File "bin\Release\icons\icon47_08.png"
  File "bin\Release\icons\icon47_09.png"
  File "bin\Release\icons\icon47_10.png"
  File "bin\Release\icons\icon47_11.png"
  File "bin\Release\icons\icon47_12.png"
  File "bin\Release\icons\icon47_13.png"
  File "bin\Release\icons\icon47_14.png"
  File "bin\Release\icons\icon47_15.png"
  File "bin\Release\icons\icon47_16.png"
  File "bin\Release\icons\icon49_03.png"
  File "bin\Release\icons\icon49_08.png"
  File "bin\Release\icons\icon49_09.png"
  File "bin\Release\icons\icon49_10.png"
  File "bin\Release\icons\icon49_11.png"
  File "bin\Release\icons\icon49_12.png"
  File "bin\Release\icons\icon49_13.png"
  File "bin\Release\icons\icon49_14.png"
  File "bin\Release\icons\icon49_15.png"
  File "bin\Release\icons\icon49_16.png"
  File "bin\Release\icons\icon50_01.png"
  File "bin\Release\icons\icon50_02.png"
  File "bin\Release\icons\icon50_04.png"
  File "bin\Release\icons\icon50_05.png"
  File "bin\Release\icons\icon50_06.png"
  File "bin\Release\icons\icon50_07.png"
  File "bin\Release\icons\icon50_08.png"
  File "bin\Release\icons\icon50_09.png"
  File "bin\Release\icons\icon50_10.png"
  File "bin\Release\icons\icon51_01.png"
  File "bin\Release\icons\icon51_02.png"
  File "bin\Release\icons\icon51_03.png"
  File "bin\Release\icons\icon51_04.png"
  File "bin\Release\icons\icon51_05.png"
  File "bin\Release\icons\icon51_06.png"
  File "bin\Release\icons\icon51_07.png"
  File "bin\Release\icons\icon51_08.png"
  File "bin\Release\icons\icon51_09.png"
  File "bin\Release\icons\icon51_10.png"
  File "bin\Release\icons\icon51_11.png"
  File "bin\Release\icons\icon51_12.png"
  File "bin\Release\icons\icon51_13.png"
  File "bin\Release\icons\icon51_14.png"
  File "bin\Release\icons\icon51_15.png"
  File "bin\Release\icons\icon51_16.png"
  File "bin\Release\icons\icon53_11.png"
  File "bin\Release\icons\icon55_12.png"
  File "bin\Release\icons\icon57_03.png"
  File "bin\Release\icons\icon57_05.png"
  File "bin\Release\icons\icon57_06.png"
  File "bin\Release\icons\icon57_07.png"
  File "bin\Release\icons\icon57_08.png"
  File "bin\Release\icons\icon57_13.png"
  File "bin\Release\icons\icon63_03.png"
  File "bin\Release\icons\icon74_14.png"
  File "bin\Release\icons\collapsed.png"
  File "bin\Release\icons\expanded.png"
  SetOutPath "$INSTDIR\images"
  File "bin\Release\images\12235.png"
  File "bin\Release\images\12236.png"
  File "bin\Release\images\16213.png"
  File "bin\Release\images\16214.png"
  File "bin\Release\images\16286.png"
  File "bin\Release\images\18005.png"
  File "bin\Release\images\18006.png"
  File "bin\Release\images\18009.png"
  File "bin\Release\images\18011.png"
  File "bin\Release\images\18646.png"
  File "bin\Release\images\18648.png"
  File "bin\Release\images\18650.png"
  File "bin\Release\images\18653.png"
  File "bin\Release\images\20059.png"
  File "bin\Release\images\20060.png"
  File "bin\Release\images\20061.png"
  File "bin\Release\images\20062.png"
  File "bin\Release\images\20063.png"
  File "bin\Release\images\20064.png"
  File "bin\Release\images\20065.png"
  File "bin\Release\images\20066.png"
  File "bin\Release\images\20522.png"
  File "bin\Release\images\27530.png"
  File "bin\Release\images\27532.png"
  File "bin\Release\images\27533.png"
  File "bin\Release\images\27535.png"
  File "bin\Release\images\27536.png"
  File "bin\Release\images\27538.png"
  File "bin\Release\images\27539.png"
  File "bin\Release\images\27540.png"
  File "bin\Release\images\27589.png"
  File "bin\Release\images\27591.png"
  File "bin\Release\images\27592.png"
  File "bin\Release\images\27594.png"
  File "bin\Release\images\27595.png"
  File "bin\Release\images\27597.png"
  File "bin\Release\images\27598.png"
  File "bin\Release\images\27600.png"
  File "bin\Release\images\27601.png"
  File "bin\Release\images\27603.png"
  File "bin\Release\images\27604.png"
  File "bin\Release\images\27606.png"
  File "bin\Release\images\27607.png"
  File "bin\Release\images\27609.png"
  File "bin\Release\images\27610.png"
  File "bin\Release\images\27612.png"
  File "bin\Release\images\27780.png"
  File "bin\Release\images\27782.png"
  File "bin\Release\images\27784.png"
  File "bin\Release\images\27786.png"
  File "bin\Release\images\27788.png"
  File "bin\Release\images\27790.png"
  File "bin\Release\images\28658.png"
  File "bin\Release\images\exalogo.png"
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "readme.txt"

; Shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\EVEPOSMon.lnk" "$INSTDIR\EVEPOSMon.exe"
  CreateShortCut "$DESKTOP\EVEPOSMon.lnk" "$INSTDIR\EVEPOSMon.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -AdditionalIcons
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk" "$INSTDIR\uninst.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\EVEPOSMon.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\EVEPOSMon.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function GetDotNETVersion
	Push $0
	Push $1

	System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1"
	StrCmp $1 "error" 0 +2
	StrCpy $0 "not found"

	Pop $1
	Exch $0
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  !insertmacro MUI_STARTMENU_GETFOLDER "Application" $ICONS_GROUP
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\readme.txt"
  Delete "$INSTDIR\images\exalogo.png"
  Delete "$INSTDIR\images\28658.png"
  Delete "$INSTDIR\images\27790.png"
  Delete "$INSTDIR\images\27788.png"
  Delete "$INSTDIR\images\27786.png"
  Delete "$INSTDIR\images\27784.png"
  Delete "$INSTDIR\images\27782.png"
  Delete "$INSTDIR\images\27780.png"
  Delete "$INSTDIR\images\27612.png"
  Delete "$INSTDIR\images\27610.png"
  Delete "$INSTDIR\images\27609.png"
  Delete "$INSTDIR\images\27607.png"
  Delete "$INSTDIR\images\27606.png"
  Delete "$INSTDIR\images\27604.png"
  Delete "$INSTDIR\images\27603.png"
  Delete "$INSTDIR\images\27601.png"
  Delete "$INSTDIR\images\27600.png"
  Delete "$INSTDIR\images\27598.png"
  Delete "$INSTDIR\images\27597.png"
  Delete "$INSTDIR\images\27595.png"
  Delete "$INSTDIR\images\27594.png"
  Delete "$INSTDIR\images\27592.png"
  Delete "$INSTDIR\images\27591.png"
  Delete "$INSTDIR\images\27589.png"
  Delete "$INSTDIR\images\27540.png"
  Delete "$INSTDIR\images\27539.png"
  Delete "$INSTDIR\images\27538.png"
  Delete "$INSTDIR\images\27536.png"
  Delete "$INSTDIR\images\27535.png"
  Delete "$INSTDIR\images\27533.png"
  Delete "$INSTDIR\images\27532.png"
  Delete "$INSTDIR\images\27530.png"
  Delete "$INSTDIR\images\20522.png"
  Delete "$INSTDIR\images\20066.png"
  Delete "$INSTDIR\images\20065.png"
  Delete "$INSTDIR\images\20064.png"
  Delete "$INSTDIR\images\20063.png"
  Delete "$INSTDIR\images\20062.png"
  Delete "$INSTDIR\images\20061.png"
  Delete "$INSTDIR\images\20060.png"
  Delete "$INSTDIR\images\20059.png"
  Delete "$INSTDIR\images\18653.png"
  Delete "$INSTDIR\images\18650.png"
  Delete "$INSTDIR\images\18648.png"
  Delete "$INSTDIR\images\18646.png"
  Delete "$INSTDIR\images\18011.png"
  Delete "$INSTDIR\images\18009.png"
  Delete "$INSTDIR\images\18006.png"
  Delete "$INSTDIR\images\18005.png"
  Delete "$INSTDIR\images\16286.png"
  Delete "$INSTDIR\images\16214.png"
  Delete "$INSTDIR\images\16213.png"
  Delete "$INSTDIR\images\12236.png"
  Delete "$INSTDIR\images\12235.png"
  Delete "$INSTDIR\icons\Thumbs.db"
  Delete "$INSTDIR\icons\icon74_14.png"
  Delete "$INSTDIR\icons\icon63_03.png"
  Delete "$INSTDIR\icons\icon57_13.png"
  Delete "$INSTDIR\icons\icon57_08.png"
  Delete "$INSTDIR\icons\icon57_07.png"
  Delete "$INSTDIR\icons\icon57_06.png"
  Delete "$INSTDIR\icons\icon57_05.png"
  Delete "$INSTDIR\icons\icon57_03.png"
  Delete "$INSTDIR\icons\icon55_12.png"
  Delete "$INSTDIR\icons\icon53_11.png"
  Delete "$INSTDIR\icons\icon51_16.png"
  Delete "$INSTDIR\icons\icon51_15.png"
  Delete "$INSTDIR\icons\icon51_14.png"
  Delete "$INSTDIR\icons\icon51_13.png"
  Delete "$INSTDIR\icons\icon51_12.png"
  Delete "$INSTDIR\icons\icon51_11.png"
  Delete "$INSTDIR\icons\icon51_10.png"
  Delete "$INSTDIR\icons\icon51_09.png"
  Delete "$INSTDIR\icons\icon51_08.png"
  Delete "$INSTDIR\icons\icon51_07.png"
  Delete "$INSTDIR\icons\icon51_06.png"
  Delete "$INSTDIR\icons\icon51_05.png"
  Delete "$INSTDIR\icons\icon51_04.png"
  Delete "$INSTDIR\icons\icon51_03.png"
  Delete "$INSTDIR\icons\icon51_02.png"
  Delete "$INSTDIR\icons\icon51_01.png"
  Delete "$INSTDIR\icons\icon50_10.png"
  Delete "$INSTDIR\icons\icon50_09.png"
  Delete "$INSTDIR\icons\icon50_08.png"
  Delete "$INSTDIR\icons\icon50_07.png"
  Delete "$INSTDIR\icons\icon50_06.png"
  Delete "$INSTDIR\icons\icon50_05.png"
  Delete "$INSTDIR\icons\icon50_04.png"
  Delete "$INSTDIR\icons\icon50_02.png"
  Delete "$INSTDIR\icons\icon50_01.png"
  Delete "$INSTDIR\icons\icon49_16.png"
  Delete "$INSTDIR\icons\icon49_15.png"
  Delete "$INSTDIR\icons\icon49_14.png"
  Delete "$INSTDIR\icons\icon49_13.png"
  Delete "$INSTDIR\icons\icon49_12.png"
  Delete "$INSTDIR\icons\icon49_11.png"
  Delete "$INSTDIR\icons\icon49_10.png"
  Delete "$INSTDIR\icons\icon49_09.png"
  Delete "$INSTDIR\icons\icon49_08.png"
  Delete "$INSTDIR\icons\icon49_03.png"
  Delete "$INSTDIR\icons\icon47_16.png"
  Delete "$INSTDIR\icons\icon47_15.png"
  Delete "$INSTDIR\icons\icon47_14.png"
  Delete "$INSTDIR\icons\icon47_13.png"
  Delete "$INSTDIR\icons\icon47_12.png"
  Delete "$INSTDIR\icons\icon47_11.png"
  Delete "$INSTDIR\icons\icon47_10.png"
  Delete "$INSTDIR\icons\icon47_09.png"
  Delete "$INSTDIR\icons\icon47_08.png"
  Delete "$INSTDIR\icons\icon47_07.png"
  Delete "$INSTDIR\icons\icon47_06.png"
  Delete "$INSTDIR\icons\icon47_05.png"
  Delete "$INSTDIR\icons\icon47_04.png"
  Delete "$INSTDIR\icons\icon47_03.png"
  Delete "$INSTDIR\icons\icon47_02.png"
  Delete "$INSTDIR\icons\icon47_01.png"
  Delete "$INSTDIR\icons\icon40_14.png"
  Delete "$INSTDIR\icons\icon34_06.png"
  Delete "$INSTDIR\icons\icon33_02.png"
  Delete "$INSTDIR\icons\icon27_10.png"
  Delete "$INSTDIR\icons\icon25_08.png"
  Delete "$INSTDIR\icons\icon24_14.png"
  Delete "$INSTDIR\icons\icon24_06.png"
  Delete "$INSTDIR\icons\icon12_07.png"
  Delete "$INSTDIR\icons\icon12_01.png"
  Delete "$INSTDIR\icons\icon10_10.png"
  Delete "$INSTDIR\icons\icon10_07.png"
  Delete "$INSTDIR\icons\icon09_08.png"
  Delete "$INSTDIR\icons\icon09_04.png"
  Delete "$INSTDIR\icons\icon09_02.png"
  Delete "$INSTDIR\icons\icon07_12.png"
  Delete "$INSTDIR\icons\icon07_09.png"
  Delete "$INSTDIR\icons\icon07_04.png"
  Delete "$INSTDIR\icons\icon06_07.png"
  Delete "$INSTDIR\icons\icon06_06.png"
  Delete "$INSTDIR\icons\icon06_03.png"
  Delete "$INSTDIR\icons\icon06_02.png"
  Delete "$INSTDIR\icons\collapsed.png"
  Delete "$INSTDIR\icons\expanded.png"
  Delete "$INSTDIR\Starbases.xml"
  Delete "$INSTDIR\AccountInfo.xml"
  Delete "$INSTDIR\FuelCost.xml"
  Delete "$INSTDIR\StarbaseList.xml"
  Delete "$INSTDIR\EVEPOSMon.vshost.exe"
  Delete "$INSTDIR\EVEPOSMon.pdb"
  Delete "$INSTDIR\EVEPOSMon.exe"
  Delete "$INSTDIR\data\moonData.xml.gz"
  Delete "$INSTDIR\data\mapData.xml.gz"
  Delete "$INSTDIR\data\invControlTowerResources.xml.gz"
  Delete "$INSTDIR\data\controlTowers.xml.gz"

  Delete "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\Website.lnk"
  Delete "$DESKTOP\EVEPOSMon.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\EVEPOSMon.lnk"

  RMDir "$SMPROGRAMS\$ICONS_GROUP"
  RMDir "$INSTDIR\images"
  RMDir "$INSTDIR\icons"
  RMDir "$INSTDIR\data"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd