//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS {
	public unsafe partial class Kernel {
		private static unsafe void SetupISR ()
		{
			byte type = (byte) (IDTEntry.Type.Present | IDTEntry.Type.Privilege_Ring_0 | IDTEntry.Type.OperandSize_32Bit | IDTEntry.Type.Interrupt_Gate);
			
			idt [0].Setup (CodeSelector, GetFunctionPointer ("ISR_0"), type);
			idt [1].Setup (CodeSelector, GetFunctionPointer ("ISR_1"), type);
			idt [2].Setup (CodeSelector, GetFunctionPointer ("ISR_2"), type);
			idt [3].Setup (CodeSelector, GetFunctionPointer ("ISR_3"), type);
			idt [4].Setup (CodeSelector, GetFunctionPointer ("ISR_4"), type);
			idt [5].Setup (CodeSelector, GetFunctionPointer ("ISR_5"), type);
			idt [6].Setup (CodeSelector, GetFunctionPointer ("ISR_6"), type);
			idt [7].Setup (CodeSelector, GetFunctionPointer ("ISR_7"), type);
			idt [8].Setup (CodeSelector, GetFunctionPointer ("ISR_8"), type);
			idt [9].Setup (CodeSelector, GetFunctionPointer ("ISR_9"), type);
			idt [10].Setup (CodeSelector, GetFunctionPointer ("ISR_10"), type);
			idt [11].Setup (CodeSelector, GetFunctionPointer ("ISR_11"), type);
			idt [12].Setup (CodeSelector, GetFunctionPointer ("ISR_12"), type);
			idt [13].Setup (CodeSelector, GetFunctionPointer ("ISR_13"), type);
			idt [14].Setup (CodeSelector, GetFunctionPointer ("ISR_14"), type);
			idt [15].Setup (CodeSelector, GetFunctionPointer ("ISR_15"), type);
			idt [16].Setup (CodeSelector, GetFunctionPointer ("ISR_16"), type);
			idt [17].Setup (CodeSelector, GetFunctionPointer ("ISR_17"), type);
			idt [18].Setup (CodeSelector, GetFunctionPointer ("ISR_18"), type);
			idt [19].Setup (CodeSelector, GetFunctionPointer ("ISR_19"), type);
			idt [20].Setup (CodeSelector, GetFunctionPointer ("ISR_20"), type);
			idt [21].Setup (CodeSelector, GetFunctionPointer ("ISR_21"), type);
			idt [22].Setup (CodeSelector, GetFunctionPointer ("ISR_22"), type);
			idt [23].Setup (CodeSelector, GetFunctionPointer ("ISR_23"), type);
			idt [24].Setup (CodeSelector, GetFunctionPointer ("ISR_24"), type);
			idt [25].Setup (CodeSelector, GetFunctionPointer ("ISR_25"), type);
			idt [26].Setup (CodeSelector, GetFunctionPointer ("ISR_26"), type);
			idt [27].Setup (CodeSelector, GetFunctionPointer ("ISR_27"), type);
			idt [28].Setup (CodeSelector, GetFunctionPointer ("ISR_28"), type);
			idt [29].Setup (CodeSelector, GetFunctionPointer ("ISR_29"), type);
			idt [30].Setup (CodeSelector, GetFunctionPointer ("ISR_30"), type);
			idt [31].Setup (CodeSelector, GetFunctionPointer ("ISR_31"), type);
			idt [32].Setup (CodeSelector, GetFunctionPointer ("ISR_32"), type);
			idt [33].Setup (CodeSelector, GetFunctionPointer ("ISR_33"), type);
			idt [34].Setup (CodeSelector, GetFunctionPointer ("ISR_34"), type);
			idt [35].Setup (CodeSelector, GetFunctionPointer ("ISR_35"), type);
			idt [36].Setup (CodeSelector, GetFunctionPointer ("ISR_36"), type);
			idt [37].Setup (CodeSelector, GetFunctionPointer ("ISR_37"), type);
			idt [38].Setup (CodeSelector, GetFunctionPointer ("ISR_38"), type);
			idt [39].Setup (CodeSelector, GetFunctionPointer ("ISR_39"), type);
			idt [40].Setup (CodeSelector, GetFunctionPointer ("ISR_40"), type);
			idt [41].Setup (CodeSelector, GetFunctionPointer ("ISR_41"), type);
			idt [42].Setup (CodeSelector, GetFunctionPointer ("ISR_42"), type);
			idt [43].Setup (CodeSelector, GetFunctionPointer ("ISR_43"), type);
			idt [44].Setup (CodeSelector, GetFunctionPointer ("ISR_44"), type);
			idt [45].Setup (CodeSelector, GetFunctionPointer ("ISR_45"), type);
			idt [46].Setup (CodeSelector, GetFunctionPointer ("ISR_46"), type);
			idt [47].Setup (CodeSelector, GetFunctionPointer ("ISR_47"), type);
			idt [48].Setup (CodeSelector, GetFunctionPointer ("ISR_48"), type);
			idt [49].Setup (CodeSelector, GetFunctionPointer ("ISR_49"), type);
			idt [50].Setup (CodeSelector, GetFunctionPointer ("ISR_50"), type);
			idt [51].Setup (CodeSelector, GetFunctionPointer ("ISR_51"), type);
			idt [52].Setup (CodeSelector, GetFunctionPointer ("ISR_52"), type);
			idt [53].Setup (CodeSelector, GetFunctionPointer ("ISR_53"), type);
			idt [54].Setup (CodeSelector, GetFunctionPointer ("ISR_54"), type);
			idt [55].Setup (CodeSelector, GetFunctionPointer ("ISR_55"), type);
			idt [56].Setup (CodeSelector, GetFunctionPointer ("ISR_56"), type);
			idt [57].Setup (CodeSelector, GetFunctionPointer ("ISR_57"), type);
			idt [58].Setup (CodeSelector, GetFunctionPointer ("ISR_58"), type);
			idt [59].Setup (CodeSelector, GetFunctionPointer ("ISR_59"), type);
			idt [60].Setup (CodeSelector, GetFunctionPointer ("ISR_60"), type);
			idt [61].Setup (CodeSelector, GetFunctionPointer ("ISR_61"), type);
			idt [62].Setup (CodeSelector, GetFunctionPointer ("ISR_62"), type);
			idt [63].Setup (CodeSelector, GetFunctionPointer ("ISR_63"), type);
			idt [64].Setup (CodeSelector, GetFunctionPointer ("ISR_64"), type);
			idt [65].Setup (CodeSelector, GetFunctionPointer ("ISR_65"), type);
			idt [66].Setup (CodeSelector, GetFunctionPointer ("ISR_66"), type);
			idt [67].Setup (CodeSelector, GetFunctionPointer ("ISR_67"), type);
			idt [68].Setup (CodeSelector, GetFunctionPointer ("ISR_68"), type);
			idt [69].Setup (CodeSelector, GetFunctionPointer ("ISR_69"), type);
			idt [70].Setup (CodeSelector, GetFunctionPointer ("ISR_70"), type);
			idt [71].Setup (CodeSelector, GetFunctionPointer ("ISR_71"), type);
			idt [72].Setup (CodeSelector, GetFunctionPointer ("ISR_72"), type);
			idt [73].Setup (CodeSelector, GetFunctionPointer ("ISR_73"), type);
			idt [74].Setup (CodeSelector, GetFunctionPointer ("ISR_74"), type);
			idt [75].Setup (CodeSelector, GetFunctionPointer ("ISR_75"), type);
			idt [76].Setup (CodeSelector, GetFunctionPointer ("ISR_76"), type);
			idt [77].Setup (CodeSelector, GetFunctionPointer ("ISR_77"), type);
			idt [78].Setup (CodeSelector, GetFunctionPointer ("ISR_78"), type);
			idt [79].Setup (CodeSelector, GetFunctionPointer ("ISR_79"), type);
			idt [80].Setup (CodeSelector, GetFunctionPointer ("ISR_80"), type);
			idt [81].Setup (CodeSelector, GetFunctionPointer ("ISR_81"), type);
			idt [82].Setup (CodeSelector, GetFunctionPointer ("ISR_82"), type);
			idt [83].Setup (CodeSelector, GetFunctionPointer ("ISR_83"), type);
			idt [84].Setup (CodeSelector, GetFunctionPointer ("ISR_84"), type);
			idt [85].Setup (CodeSelector, GetFunctionPointer ("ISR_85"), type);
			idt [86].Setup (CodeSelector, GetFunctionPointer ("ISR_86"), type);
			idt [87].Setup (CodeSelector, GetFunctionPointer ("ISR_87"), type);
			idt [88].Setup (CodeSelector, GetFunctionPointer ("ISR_88"), type);
			idt [89].Setup (CodeSelector, GetFunctionPointer ("ISR_89"), type);
			idt [90].Setup (CodeSelector, GetFunctionPointer ("ISR_90"), type);
			idt [91].Setup (CodeSelector, GetFunctionPointer ("ISR_91"), type);
			idt [92].Setup (CodeSelector, GetFunctionPointer ("ISR_92"), type);
			idt [93].Setup (CodeSelector, GetFunctionPointer ("ISR_93"), type);
			idt [94].Setup (CodeSelector, GetFunctionPointer ("ISR_94"), type);
			idt [95].Setup (CodeSelector, GetFunctionPointer ("ISR_95"), type);
			idt [96].Setup (CodeSelector, GetFunctionPointer ("ISR_96"), type);
			idt [97].Setup (CodeSelector, GetFunctionPointer ("ISR_97"), type);
			idt [98].Setup (CodeSelector, GetFunctionPointer ("ISR_98"), type);
			idt [99].Setup (CodeSelector, GetFunctionPointer ("ISR_99"), type);
			idt [100].Setup (CodeSelector, GetFunctionPointer ("ISR_100"), type);
			idt [101].Setup (CodeSelector, GetFunctionPointer ("ISR_101"), type);
			idt [102].Setup (CodeSelector, GetFunctionPointer ("ISR_102"), type);
			idt [103].Setup (CodeSelector, GetFunctionPointer ("ISR_103"), type);
			idt [104].Setup (CodeSelector, GetFunctionPointer ("ISR_104"), type);
			idt [105].Setup (CodeSelector, GetFunctionPointer ("ISR_105"), type);
			idt [106].Setup (CodeSelector, GetFunctionPointer ("ISR_106"), type);
			idt [107].Setup (CodeSelector, GetFunctionPointer ("ISR_107"), type);
			idt [108].Setup (CodeSelector, GetFunctionPointer ("ISR_108"), type);
			idt [109].Setup (CodeSelector, GetFunctionPointer ("ISR_109"), type);
			idt [110].Setup (CodeSelector, GetFunctionPointer ("ISR_110"), type);
			idt [111].Setup (CodeSelector, GetFunctionPointer ("ISR_111"), type);
			idt [112].Setup (CodeSelector, GetFunctionPointer ("ISR_112"), type);
			idt [113].Setup (CodeSelector, GetFunctionPointer ("ISR_113"), type);
			idt [114].Setup (CodeSelector, GetFunctionPointer ("ISR_114"), type);
			idt [115].Setup (CodeSelector, GetFunctionPointer ("ISR_115"), type);
			idt [116].Setup (CodeSelector, GetFunctionPointer ("ISR_116"), type);
			idt [117].Setup (CodeSelector, GetFunctionPointer ("ISR_117"), type);
			idt [118].Setup (CodeSelector, GetFunctionPointer ("ISR_118"), type);
			idt [119].Setup (CodeSelector, GetFunctionPointer ("ISR_119"), type);
			idt [120].Setup (CodeSelector, GetFunctionPointer ("ISR_120"), type);
			idt [121].Setup (CodeSelector, GetFunctionPointer ("ISR_121"), type);
			idt [122].Setup (CodeSelector, GetFunctionPointer ("ISR_122"), type);
			idt [123].Setup (CodeSelector, GetFunctionPointer ("ISR_123"), type);
			idt [124].Setup (CodeSelector, GetFunctionPointer ("ISR_124"), type);
			idt [125].Setup (CodeSelector, GetFunctionPointer ("ISR_125"), type);
			idt [126].Setup (CodeSelector, GetFunctionPointer ("ISR_126"), type);
			idt [127].Setup (CodeSelector, GetFunctionPointer ("ISR_127"), type);
			idt [128].Setup (CodeSelector, GetFunctionPointer ("ISR_128"), type);
			idt [129].Setup (CodeSelector, GetFunctionPointer ("ISR_129"), type);
			idt [130].Setup (CodeSelector, GetFunctionPointer ("ISR_130"), type);
			idt [131].Setup (CodeSelector, GetFunctionPointer ("ISR_131"), type);
			idt [132].Setup (CodeSelector, GetFunctionPointer ("ISR_132"), type);
			idt [133].Setup (CodeSelector, GetFunctionPointer ("ISR_133"), type);
			idt [134].Setup (CodeSelector, GetFunctionPointer ("ISR_134"), type);
			idt [135].Setup (CodeSelector, GetFunctionPointer ("ISR_135"), type);
			idt [136].Setup (CodeSelector, GetFunctionPointer ("ISR_136"), type);
			idt [137].Setup (CodeSelector, GetFunctionPointer ("ISR_137"), type);
			idt [138].Setup (CodeSelector, GetFunctionPointer ("ISR_138"), type);
			idt [139].Setup (CodeSelector, GetFunctionPointer ("ISR_139"), type);
			idt [140].Setup (CodeSelector, GetFunctionPointer ("ISR_140"), type);
			idt [141].Setup (CodeSelector, GetFunctionPointer ("ISR_141"), type);
			idt [142].Setup (CodeSelector, GetFunctionPointer ("ISR_142"), type);
			idt [143].Setup (CodeSelector, GetFunctionPointer ("ISR_143"), type);
			idt [144].Setup (CodeSelector, GetFunctionPointer ("ISR_144"), type);
			idt [145].Setup (CodeSelector, GetFunctionPointer ("ISR_145"), type);
			idt [146].Setup (CodeSelector, GetFunctionPointer ("ISR_146"), type);
			idt [147].Setup (CodeSelector, GetFunctionPointer ("ISR_147"), type);
			idt [148].Setup (CodeSelector, GetFunctionPointer ("ISR_148"), type);
			idt [149].Setup (CodeSelector, GetFunctionPointer ("ISR_149"), type);
			idt [150].Setup (CodeSelector, GetFunctionPointer ("ISR_150"), type);
			idt [151].Setup (CodeSelector, GetFunctionPointer ("ISR_151"), type);
			idt [152].Setup (CodeSelector, GetFunctionPointer ("ISR_152"), type);
			idt [153].Setup (CodeSelector, GetFunctionPointer ("ISR_153"), type);
			idt [154].Setup (CodeSelector, GetFunctionPointer ("ISR_154"), type);
			idt [155].Setup (CodeSelector, GetFunctionPointer ("ISR_155"), type);
			idt [156].Setup (CodeSelector, GetFunctionPointer ("ISR_156"), type);
			idt [157].Setup (CodeSelector, GetFunctionPointer ("ISR_157"), type);
			idt [158].Setup (CodeSelector, GetFunctionPointer ("ISR_158"), type);
			idt [159].Setup (CodeSelector, GetFunctionPointer ("ISR_159"), type);
			idt [160].Setup (CodeSelector, GetFunctionPointer ("ISR_160"), type);
			idt [161].Setup (CodeSelector, GetFunctionPointer ("ISR_161"), type);
			idt [162].Setup (CodeSelector, GetFunctionPointer ("ISR_162"), type);
			idt [163].Setup (CodeSelector, GetFunctionPointer ("ISR_163"), type);
			idt [164].Setup (CodeSelector, GetFunctionPointer ("ISR_164"), type);
			idt [165].Setup (CodeSelector, GetFunctionPointer ("ISR_165"), type);
			idt [166].Setup (CodeSelector, GetFunctionPointer ("ISR_166"), type);
			idt [167].Setup (CodeSelector, GetFunctionPointer ("ISR_167"), type);
			idt [168].Setup (CodeSelector, GetFunctionPointer ("ISR_168"), type);
			idt [169].Setup (CodeSelector, GetFunctionPointer ("ISR_169"), type);
			idt [170].Setup (CodeSelector, GetFunctionPointer ("ISR_170"), type);
			idt [171].Setup (CodeSelector, GetFunctionPointer ("ISR_171"), type);
			idt [172].Setup (CodeSelector, GetFunctionPointer ("ISR_172"), type);
			idt [173].Setup (CodeSelector, GetFunctionPointer ("ISR_173"), type);
			idt [174].Setup (CodeSelector, GetFunctionPointer ("ISR_174"), type);
			idt [175].Setup (CodeSelector, GetFunctionPointer ("ISR_175"), type);
			idt [176].Setup (CodeSelector, GetFunctionPointer ("ISR_176"), type);
			idt [177].Setup (CodeSelector, GetFunctionPointer ("ISR_177"), type);
			idt [178].Setup (CodeSelector, GetFunctionPointer ("ISR_178"), type);
			idt [179].Setup (CodeSelector, GetFunctionPointer ("ISR_179"), type);
			idt [180].Setup (CodeSelector, GetFunctionPointer ("ISR_180"), type);
			idt [181].Setup (CodeSelector, GetFunctionPointer ("ISR_181"), type);
			idt [182].Setup (CodeSelector, GetFunctionPointer ("ISR_182"), type);
			idt [183].Setup (CodeSelector, GetFunctionPointer ("ISR_183"), type);
			idt [184].Setup (CodeSelector, GetFunctionPointer ("ISR_184"), type);
			idt [185].Setup (CodeSelector, GetFunctionPointer ("ISR_185"), type);
			idt [186].Setup (CodeSelector, GetFunctionPointer ("ISR_186"), type);
			idt [187].Setup (CodeSelector, GetFunctionPointer ("ISR_187"), type);
			idt [188].Setup (CodeSelector, GetFunctionPointer ("ISR_188"), type);
			idt [189].Setup (CodeSelector, GetFunctionPointer ("ISR_189"), type);
			idt [190].Setup (CodeSelector, GetFunctionPointer ("ISR_190"), type);
			idt [191].Setup (CodeSelector, GetFunctionPointer ("ISR_191"), type);
			idt [192].Setup (CodeSelector, GetFunctionPointer ("ISR_192"), type);
			idt [193].Setup (CodeSelector, GetFunctionPointer ("ISR_193"), type);
			idt [194].Setup (CodeSelector, GetFunctionPointer ("ISR_194"), type);
			idt [195].Setup (CodeSelector, GetFunctionPointer ("ISR_195"), type);
			idt [196].Setup (CodeSelector, GetFunctionPointer ("ISR_196"), type);
			idt [197].Setup (CodeSelector, GetFunctionPointer ("ISR_197"), type);
			idt [198].Setup (CodeSelector, GetFunctionPointer ("ISR_198"), type);
			idt [199].Setup (CodeSelector, GetFunctionPointer ("ISR_199"), type);
			idt [200].Setup (CodeSelector, GetFunctionPointer ("ISR_200"), type);
			idt [201].Setup (CodeSelector, GetFunctionPointer ("ISR_201"), type);
			idt [202].Setup (CodeSelector, GetFunctionPointer ("ISR_202"), type);
			idt [203].Setup (CodeSelector, GetFunctionPointer ("ISR_203"), type);
			idt [204].Setup (CodeSelector, GetFunctionPointer ("ISR_204"), type);
			idt [205].Setup (CodeSelector, GetFunctionPointer ("ISR_205"), type);
			idt [206].Setup (CodeSelector, GetFunctionPointer ("ISR_206"), type);
			idt [207].Setup (CodeSelector, GetFunctionPointer ("ISR_207"), type);
			idt [208].Setup (CodeSelector, GetFunctionPointer ("ISR_208"), type);
			idt [209].Setup (CodeSelector, GetFunctionPointer ("ISR_209"), type);
			idt [210].Setup (CodeSelector, GetFunctionPointer ("ISR_210"), type);
			idt [211].Setup (CodeSelector, GetFunctionPointer ("ISR_211"), type);
			idt [212].Setup (CodeSelector, GetFunctionPointer ("ISR_212"), type);
			idt [213].Setup (CodeSelector, GetFunctionPointer ("ISR_213"), type);
			idt [214].Setup (CodeSelector, GetFunctionPointer ("ISR_214"), type);
			idt [215].Setup (CodeSelector, GetFunctionPointer ("ISR_215"), type);
			idt [216].Setup (CodeSelector, GetFunctionPointer ("ISR_216"), type);
			idt [217].Setup (CodeSelector, GetFunctionPointer ("ISR_217"), type);
			idt [218].Setup (CodeSelector, GetFunctionPointer ("ISR_218"), type);
			idt [219].Setup (CodeSelector, GetFunctionPointer ("ISR_219"), type);
			idt [220].Setup (CodeSelector, GetFunctionPointer ("ISR_220"), type);
			idt [221].Setup (CodeSelector, GetFunctionPointer ("ISR_221"), type);
			idt [222].Setup (CodeSelector, GetFunctionPointer ("ISR_222"), type);
			idt [223].Setup (CodeSelector, GetFunctionPointer ("ISR_223"), type);
			idt [224].Setup (CodeSelector, GetFunctionPointer ("ISR_224"), type);
			idt [225].Setup (CodeSelector, GetFunctionPointer ("ISR_225"), type);
			idt [226].Setup (CodeSelector, GetFunctionPointer ("ISR_226"), type);
			idt [227].Setup (CodeSelector, GetFunctionPointer ("ISR_227"), type);
			idt [228].Setup (CodeSelector, GetFunctionPointer ("ISR_228"), type);
			idt [229].Setup (CodeSelector, GetFunctionPointer ("ISR_229"), type);
			idt [230].Setup (CodeSelector, GetFunctionPointer ("ISR_230"), type);
			idt [231].Setup (CodeSelector, GetFunctionPointer ("ISR_231"), type);
			idt [232].Setup (CodeSelector, GetFunctionPointer ("ISR_232"), type);
			idt [233].Setup (CodeSelector, GetFunctionPointer ("ISR_233"), type);
			idt [234].Setup (CodeSelector, GetFunctionPointer ("ISR_234"), type);
			idt [235].Setup (CodeSelector, GetFunctionPointer ("ISR_235"), type);
			idt [236].Setup (CodeSelector, GetFunctionPointer ("ISR_236"), type);
			idt [237].Setup (CodeSelector, GetFunctionPointer ("ISR_237"), type);
			idt [238].Setup (CodeSelector, GetFunctionPointer ("ISR_238"), type);
			idt [239].Setup (CodeSelector, GetFunctionPointer ("ISR_239"), type);
			idt [240].Setup (CodeSelector, GetFunctionPointer ("ISR_240"), type);
			idt [241].Setup (CodeSelector, GetFunctionPointer ("ISR_241"), type);
			idt [242].Setup (CodeSelector, GetFunctionPointer ("ISR_242"), type);
			idt [243].Setup (CodeSelector, GetFunctionPointer ("ISR_243"), type);
			idt [244].Setup (CodeSelector, GetFunctionPointer ("ISR_244"), type);
			idt [245].Setup (CodeSelector, GetFunctionPointer ("ISR_245"), type);
			idt [246].Setup (CodeSelector, GetFunctionPointer ("ISR_246"), type);
			idt [247].Setup (CodeSelector, GetFunctionPointer ("ISR_247"), type);
			idt [248].Setup (CodeSelector, GetFunctionPointer ("ISR_248"), type);
			idt [249].Setup (CodeSelector, GetFunctionPointer ("ISR_249"), type);
			idt [250].Setup (CodeSelector, GetFunctionPointer ("ISR_250"), type);
			idt [251].Setup (CodeSelector, GetFunctionPointer ("ISR_251"), type);
			idt [252].Setup (CodeSelector, GetFunctionPointer ("ISR_252"), type);
			idt [253].Setup (CodeSelector, GetFunctionPointer ("ISR_253"), type);
			idt [254].Setup (CodeSelector, GetFunctionPointer ("ISR_254"), type);
			idt [255].Setup (CodeSelector, GetFunctionPointer ("ISR_255"), type);
		}
		
		private static unsafe void ISRHandlers ()
		{
			Asm.LABEL ("ISR_0");
			Asm.PUSH (0);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_1");
			Asm.PUSH (1);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_2");
			Asm.PUSH (2);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_3");
			Asm.PUSH (3);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_4");
			Asm.PUSH (4);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_5");
			Asm.PUSH (5);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_6");
			Asm.PUSH (6);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_7");
			Asm.PUSH (7);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_8");
			Asm.PUSH (8);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_9");
			Asm.PUSH (9);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_10");
			Asm.PUSH (10);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_11");
			Asm.PUSH (11);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_12");
			Asm.PUSH (12);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_13");
			Asm.PUSH (13);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_14");
			Asm.PUSH (14);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_15");
			Asm.PUSH (15);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_16");
			Asm.PUSH (16);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_17");
			Asm.PUSH (17);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_18");
			Asm.PUSH (18);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_19");
			Asm.PUSH (19);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_20");
			Asm.PUSH (20);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_21");
			Asm.PUSH (21);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_22");
			Asm.PUSH (22);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_23");
			Asm.PUSH (23);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_24");
			Asm.PUSH (24);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_25");
			Asm.PUSH (25);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_26");
			Asm.PUSH (26);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_27");
			Asm.PUSH (27);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_28");
			Asm.PUSH (28);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_29");
			Asm.PUSH (29);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_30");
			Asm.PUSH (30);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_31");
			Asm.PUSH (31);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_32");
			Asm.PUSH (32);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_33");
			Asm.PUSH (33);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_34");
			Asm.PUSH (34);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_35");
			Asm.PUSH (35);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_36");
			Asm.PUSH (36);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_37");
			Asm.PUSH (37);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_38");
			Asm.PUSH (38);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_39");
			Asm.PUSH (39);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_40");
			Asm.PUSH (40);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_41");
			Asm.PUSH (41);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_42");
			Asm.PUSH (42);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_43");
			Asm.PUSH (43);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_44");
			Asm.PUSH (44);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_45");
			Asm.PUSH (45);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_46");
			Asm.PUSH (46);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_47");
			Asm.PUSH (47);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_48");
			Asm.PUSH (48);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_49");
			Asm.PUSH (49);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_50");
			Asm.PUSH (50);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_51");
			Asm.PUSH (51);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_52");
			Asm.PUSH (52);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_53");
			Asm.PUSH (53);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_54");
			Asm.PUSH (54);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_55");
			Asm.PUSH (55);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_56");
			Asm.PUSH (56);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_57");
			Asm.PUSH (57);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_58");
			Asm.PUSH (58);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_59");
			Asm.PUSH (59);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_60");
			Asm.PUSH (60);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_61");
			Asm.PUSH (61);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_62");
			Asm.PUSH (62);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_63");
			Asm.PUSH (63);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_64");
			Asm.PUSH (64);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_65");
			Asm.PUSH (65);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_66");
			Asm.PUSH (66);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_67");
			Asm.PUSH (67);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_68");
			Asm.PUSH (68);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_69");
			Asm.PUSH (69);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_70");
			Asm.PUSH (70);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_71");
			Asm.PUSH (71);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_72");
			Asm.PUSH (72);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_73");
			Asm.PUSH (73);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_74");
			Asm.PUSH (74);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_75");
			Asm.PUSH (75);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_76");
			Asm.PUSH (76);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_77");
			Asm.PUSH (77);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_78");
			Asm.PUSH (78);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_79");
			Asm.PUSH (79);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_80");
			Asm.PUSH (80);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_81");
			Asm.PUSH (81);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_82");
			Asm.PUSH (82);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_83");
			Asm.PUSH (83);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_84");
			Asm.PUSH (84);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_85");
			Asm.PUSH (85);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_86");
			Asm.PUSH (86);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_87");
			Asm.PUSH (87);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_88");
			Asm.PUSH (88);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_89");
			Asm.PUSH (89);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_90");
			Asm.PUSH (90);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_91");
			Asm.PUSH (91);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_92");
			Asm.PUSH (92);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_93");
			Asm.PUSH (93);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_94");
			Asm.PUSH (94);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_95");
			Asm.PUSH (95);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_96");
			Asm.PUSH (96);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_97");
			Asm.PUSH (97);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_98");
			Asm.PUSH (98);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_99");
			Asm.PUSH (99);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_100");
			Asm.PUSH (100);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_101");
			Asm.PUSH (101);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_102");
			Asm.PUSH (102);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_103");
			Asm.PUSH (103);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_104");
			Asm.PUSH (104);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_105");
			Asm.PUSH (105);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_106");
			Asm.PUSH (106);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_107");
			Asm.PUSH (107);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_108");
			Asm.PUSH (108);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_109");
			Asm.PUSH (109);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_110");
			Asm.PUSH (110);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_111");
			Asm.PUSH (111);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_112");
			Asm.PUSH (112);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_113");
			Asm.PUSH (113);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_114");
			Asm.PUSH (114);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_115");
			Asm.PUSH (115);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_116");
			Asm.PUSH (116);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_117");
			Asm.PUSH (117);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_118");
			Asm.PUSH (118);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_119");
			Asm.PUSH (119);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_120");
			Asm.PUSH (120);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_121");
			Asm.PUSH (121);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_122");
			Asm.PUSH (122);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_123");
			Asm.PUSH (123);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_124");
			Asm.PUSH (124);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_125");
			Asm.PUSH (125);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_126");
			Asm.PUSH (126);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_127");
			Asm.PUSH (127);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_128");
			Asm.PUSH (128);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_129");
			Asm.PUSH (129);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_130");
			Asm.PUSH (130);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_131");
			Asm.PUSH (131);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_132");
			Asm.PUSH (132);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_133");
			Asm.PUSH (133);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_134");
			Asm.PUSH (134);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_135");
			Asm.PUSH (135);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_136");
			Asm.PUSH (136);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_137");
			Asm.PUSH (137);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_138");
			Asm.PUSH (138);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_139");
			Asm.PUSH (139);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_140");
			Asm.PUSH (140);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_141");
			Asm.PUSH (141);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_142");
			Asm.PUSH (142);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_143");
			Asm.PUSH (143);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_144");
			Asm.PUSH (144);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_145");
			Asm.PUSH (145);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_146");
			Asm.PUSH (146);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_147");
			Asm.PUSH (147);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_148");
			Asm.PUSH (148);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_149");
			Asm.PUSH (149);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_150");
			Asm.PUSH (150);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_151");
			Asm.PUSH (151);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_152");
			Asm.PUSH (152);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_153");
			Asm.PUSH (153);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_154");
			Asm.PUSH (154);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_155");
			Asm.PUSH (155);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_156");
			Asm.PUSH (156);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_157");
			Asm.PUSH (157);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_158");
			Asm.PUSH (158);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_159");
			Asm.PUSH (159);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_160");
			Asm.PUSH (160);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_161");
			Asm.PUSH (161);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_162");
			Asm.PUSH (162);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_163");
			Asm.PUSH (163);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_164");
			Asm.PUSH (164);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_165");
			Asm.PUSH (165);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_166");
			Asm.PUSH (166);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_167");
			Asm.PUSH (167);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_168");
			Asm.PUSH (168);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_169");
			Asm.PUSH (169);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_170");
			Asm.PUSH (170);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_171");
			Asm.PUSH (171);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_172");
			Asm.PUSH (172);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_173");
			Asm.PUSH (173);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_174");
			Asm.PUSH (174);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_175");
			Asm.PUSH (175);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_176");
			Asm.PUSH (176);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_177");
			Asm.PUSH (177);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_178");
			Asm.PUSH (178);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_179");
			Asm.PUSH (179);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_180");
			Asm.PUSH (180);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_181");
			Asm.PUSH (181);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_182");
			Asm.PUSH (182);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_183");
			Asm.PUSH (183);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_184");
			Asm.PUSH (184);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_185");
			Asm.PUSH (185);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_186");
			Asm.PUSH (186);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_187");
			Asm.PUSH (187);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_188");
			Asm.PUSH (188);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_189");
			Asm.PUSH (189);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_190");
			Asm.PUSH (190);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_191");
			Asm.PUSH (191);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_192");
			Asm.PUSH (192);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_193");
			Asm.PUSH (193);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_194");
			Asm.PUSH (194);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_195");
			Asm.PUSH (195);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_196");
			Asm.PUSH (196);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_197");
			Asm.PUSH (197);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_198");
			Asm.PUSH (198);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_199");
			Asm.PUSH (199);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_200");
			Asm.PUSH (200);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_201");
			Asm.PUSH (201);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_202");
			Asm.PUSH (202);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_203");
			Asm.PUSH (203);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_204");
			Asm.PUSH (204);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_205");
			Asm.PUSH (205);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_206");
			Asm.PUSH (206);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_207");
			Asm.PUSH (207);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_208");
			Asm.PUSH (208);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_209");
			Asm.PUSH (209);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_210");
			Asm.PUSH (210);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_211");
			Asm.PUSH (211);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_212");
			Asm.PUSH (212);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_213");
			Asm.PUSH (213);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_214");
			Asm.PUSH (214);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_215");
			Asm.PUSH (215);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_216");
			Asm.PUSH (216);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_217");
			Asm.PUSH (217);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_218");
			Asm.PUSH (218);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_219");
			Asm.PUSH (219);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_220");
			Asm.PUSH (220);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_221");
			Asm.PUSH (221);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_222");
			Asm.PUSH (222);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_223");
			Asm.PUSH (223);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_224");
			Asm.PUSH (224);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_225");
			Asm.PUSH (225);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_226");
			Asm.PUSH (226);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_227");
			Asm.PUSH (227);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_228");
			Asm.PUSH (228);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_229");
			Asm.PUSH (229);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_230");
			Asm.PUSH (230);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_231");
			Asm.PUSH (231);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_232");
			Asm.PUSH (232);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_233");
			Asm.PUSH (233);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_234");
			Asm.PUSH (234);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_235");
			Asm.PUSH (235);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_236");
			Asm.PUSH (236);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_237");
			Asm.PUSH (237);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_238");
			Asm.PUSH (238);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_239");
			Asm.PUSH (239);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_240");
			Asm.PUSH (240);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_241");
			Asm.PUSH (241);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_242");
			Asm.PUSH (242);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_243");
			Asm.PUSH (243);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_244");
			Asm.PUSH (244);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_245");
			Asm.PUSH (245);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_246");
			Asm.PUSH (246);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_247");
			Asm.PUSH (247);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_248");
			Asm.PUSH (248);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_249");
			Asm.PUSH (249);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_250");
			Asm.PUSH (250);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_251");
			Asm.PUSH (251);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_252");
			Asm.PUSH (252);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_253");
			Asm.PUSH (253);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_254");
			Asm.PUSH (254);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISR_255");
			Asm.PUSH (255);
			Asm.JMP ("ISRDispatcher");
			
			Asm.LABEL ("ISRDispatcher");
			Asm.CLI ();
			Asm.PUSHA ();
			Asm.PUSH (Seg.DS);
			Asm.PUSH (Seg.ES);
			Asm.PUSH (Seg.GS);
			Asm.PUSH (Seg.FS);
			Asm.PUSH (Seg.SS);
			Asm.CALL ("System.Void SharpOS.Kernel.ISRDispatcher SharpOS.ISRData");
			Asm.POP (Seg.SS);
			Asm.POP (Seg.FS);
			Asm.POP (Seg.GS);
			Asm.POP (Seg.ES);
			Asm.POP (Seg.DS);
			Asm.POPA ();
			Asm.ADD (R32.ESP, 0x04);
			Asm.STI ();
			Asm.IRET ();
		}
	}
}
